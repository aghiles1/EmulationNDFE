package polytech.unice.fr.smartcard.nfc

import android.util.Log

class CCFile {

    private val ccLen = byteArrayOf(0x00.toByte(), 0x0F.toByte())
    private val mappingVersion = 0x20.toByte()
    val mLe = byteArrayOf(0x00.toByte(), 0x3B.toByte())
    private val mLc = byteArrayOf(0x00.toByte(), 0x34.toByte())
    private val tlvBlock = byteArrayOf(0x04.toByte(), 0x06.toByte(), 0x81.toByte(), 0x01.toByte(),
            0x10.toByte(), 0x00.toByte(), 0x00.toByte(), 0x00.toByte())

    fun getContent(): ByteArray {
        Log.e("CCFile","Get Content")
        return this.ccLen + this.mappingVersion + this.mLe + this.mLc + this.tlvBlock
    }

}