package polytech.unice.fr.smartcard.nfc

import android.nfc.cardemulation.HostApduService
import android.os.Bundle
import android.util.Log
import java.nio.ByteBuffer
import java.util.*

class CardService : HostApduService() {

    companion object {
        private const val SELECT = 0xA4.toByte()

        private const val SELECT_APP_P1 = 0x04.toByte()
        private const val SELECT_APP_P2 = 0x00.toByte()
        private val SELECT_APP_LC = IntRange(5, 16)
        private val SELECT_APP_AID = byteArrayOf(0xD2.toByte(), 0x76.toByte(), 0x00.toByte(), 0x00.toByte(), 0x85.toByte(), 0x01.toByte(), 0x01.toByte())

        private const val SELECT_FILE_P1 = 0x00.toByte()
        private const val SELECT_FILE_P2 = 0x0C.toByte()
        private const val SELECT_FILE_LC = 2
        private val CC_FILE = byteArrayOf(0xE1.toByte(), 0x03.toByte())
        private val NDEF = byteArrayOf(0x81.toByte(), 0x01.toByte())

        private const val READ = 0xB0.toByte()
        private const val UPDATE = 0xD6.toByte()
    }

    private var selected = Selected.NONE
    private var ccFile = CCFile()
    private var ndef = NDefFile()

    override fun onDeactivated(reason: Int) {}

    override fun processCommandApdu(commandApdu: ByteArray?, extras: Bundle?): ByteArray {
        return commandApdu?.let { this.decode(it) } ?: byteArrayOf()
    }

    private fun decode(cmd: ByteArray): ByteArray {

        Log.e("CardService", "Decode")

        /* 0 : CLA, 1 : INS, 2 : P1, 3 : P2 */

        if (cmd.size < 4) return ErrorCode.ACCESS_KO.code //Todo : don't know the error

        if (cmd[0] != 0x00.toByte()) return ErrorCode.UNKNOWN_CLA.code

        return when (cmd[1]) {
            SELECT -> selectCmd(cmd[2], cmd[3], cmd.drop(4).toByteArray()).code
            READ -> readCmd(cmd[2], cmd[3], cmd.drop(4).toByteArray())
            UPDATE -> updateCmd(cmd[2], cmd[3], cmd.drop(4).toByteArray())
            else -> ErrorCode.UNKNOWN_INS.code
        }

    }

    private fun selectCmd(p1: Byte, p2: Byte, content: ByteArray): ErrorCode {

        Log.e("CardService", "Select")

        val lc = content[0].toInt()
        val data = Arrays.copyOfRange(content, 1, lc + 1)

        when {
            p1 == SELECT_APP_P1 && p2 == SELECT_APP_P2 -> {     /* Select Application */

                Log.e("CardService", "Select Application")

                return when {
                    !SELECT_APP_LC.contains(lc) -> ErrorCode.INCORRECT_LC
                    !SELECT_APP_AID.contentEquals(data) -> ErrorCode.UNKNOWN_AID_LID
                    else -> {
                        this.selected = Selected.APPLICATION
                        ErrorCode.OK
                    }
                }
            }
            p1 == SELECT_FILE_P1 && p2 == SELECT_FILE_P2 -> {   /* Select File */

                Log.e("CardService", "Select File")

                return when {
                    this.selected == Selected.NONE -> ErrorCode.INCORRECT_STATE
                    SELECT_FILE_LC != lc -> ErrorCode.INCORRECT_LC
                    CC_FILE.contentEquals(data) -> {
                        this.selected = Selected.CC_FILE
                        ErrorCode.OK
                    }
                    NDEF.contentEquals(data) -> {
                        this.selected = Selected.NDEF
                        ErrorCode.OK
                    }
                    else -> ErrorCode.UNKNOWN_AID_LID
                }
            }
            else -> return ErrorCode.INCORRECT_P1_P2_SELECT
        }
    }

    private fun readCmd(p1: Byte, p2: Byte, content: ByteArray): ByteArray {

        Log.e("CardService", "Read")

        val offset = (p1.toInt() shl 8) + p2.toInt()
        val le = content[0].toInt()
        val range = IntRange(offset, offset + le - 1)

        when (this.selected) {

            Selected.CC_FILE -> {

                Log.e("CardService", "Read CC File")

                val fileBytes = this.ccFile.getContent()

                return when {
                    (offset + le) > fileBytes.size -> ErrorCode.INCORRECT_OFFSET_LE.code
                    le > ByteBuffer.wrap(this.ccFile.mLe).int -> ErrorCode.INCORRECT_LE.code
                    else -> fileBytes.sliceArray(range) + ErrorCode.OK.code
                }

            }

            Selected.NDEF -> {

                Log.e("CardService", "Read NDEF File")

                val fileBytes = this.ndef.getContent()

                return when {
                    offset + le > fileBytes.size -> ErrorCode.INCORRECT_OFFSET_LE.code
                    le > ByteBuffer.wrap(this.ccFile.mLe).int -> ErrorCode.INCORRECT_LC.code
                    else -> fileBytes.sliceArray(range) + ErrorCode.OK.code
                }

            }

            else -> return ErrorCode.INCORRECT_STATE.code

        }
    }

    private fun updateCmd(p1: Byte, p2: Byte, content: ByteArray): ByteArray {

        Log.e("CardService", "Update")

        val offset = (p1.toInt() shl 8) + p2.toInt()
        val le = content[0].toInt()
        val data = content.sliceArray(IntRange(1, content.size - 1))

        val fileBytes = this.ndef.getContent()

        return when {
            this.selected != Selected.NDEF -> ErrorCode.INCORRECT_STATE.code
            offset + le > fileBytes.size -> ErrorCode.INCORRECT_OFFSET_LC.code
            le > ByteBuffer.wrap(this.ccFile.mLe).int -> ErrorCode.INCORRECT_LC.code
            else -> {
                val pre = fileBytes.sliceArray(IntRange(0, offset - 1))
                val post = fileBytes.sliceArray(IntRange(offset + le, content.size - 1))
                this.ndef.write(pre + data + post)
                ErrorCode.OK.code
            }
        }
    }

}
