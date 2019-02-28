package polytech.unice.fr.smartcard.nfc

enum class ErrorCode(val code: ByteArray) {

    OK(byteArrayOf(0x90.toByte(), 0x00.toByte())),
    UNKNOWN_CLA(byteArrayOf(0x6E.toByte(), 0x00.toByte())),
    UNKNOWN_INS(byteArrayOf(0x6D.toByte(), 0x00.toByte())),
    INCORRECT_LC(byteArrayOf(0x67.toByte(), 0x00.toByte())),
    INCORRECT_LE(byteArrayOf(0x6C.toByte(), 0x00.toByte())),
    UNKNOWN_AID_LID(byteArrayOf(0x6A.toByte(), 0x82.toByte())),
    INCORRECT_STATE(byteArrayOf(0x69.toByte(), 0x86.toByte())),
    INCORRECT_P1_P2_SELECT(byteArrayOf(0x6A.toByte(), 0x86.toByte())),
    INVALID_P1_P2_READ_UPDATE(byteArrayOf(0x6B.toByte(), 0x00.toByte())),
    INCORRECT_OFFSET_LC(byteArrayOf(0x6A.toByte(), 0x87.toByte())),
    INCORRECT_OFFSET_LE(byteArrayOf(0x6C.toByte(), 0x00.toByte())),
    ACCESS_KO(byteArrayOf(0x6C.toByte(), 0x00.toByte())),
    EOF(byteArrayOf(0x62.toByte(), 0x82.toByte())),

}
