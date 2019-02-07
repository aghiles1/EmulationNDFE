package fr.unice.polytech

import android.os.Bundle
import android.nfc.cardemulation.HostApduService

class CardService : HostApduService() {

    override fun processCommandApdu(byAPDU: ByteArray, extras: Bundle): ByteArray {
        return byteArrayOf(0x90.toByte(), 0x00.toByte())
    }

    override fun onDeactivated(reason: Int) {
    }
}