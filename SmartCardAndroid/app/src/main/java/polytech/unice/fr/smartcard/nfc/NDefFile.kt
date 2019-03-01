package polytech.unice.fr.smartcard.nfc

import android.util.Log
import polytech.unice.fr.smartcard.SmartCardApp

class NDefFile private constructor() {

    companion object {

        private var inst: NDefFile? = null

        fun getInstance(): NDefFile {
            return inst?.let { it } ?: NDefFile()
        }

    }

    private val sharedPref = SmartCardApp.instance!!.getSharedPreferences("NDefFile", 0)

    fun getContent(): ByteArray {
        Log.e("NDefFile","Get Content")
        return if (this.sharedPref.contains("data")) {
            this.sharedPref.getString("data", "").toByteArray()
        } else {
            byteArrayOf()
        }
    }

    fun write(bytes: ByteArray) {
        Log.e("NDefFile","Write Content")
        val edit = this.sharedPref.edit()
        edit.putString("data", bytes.joinToString("") { String.format("%02x", it) })
        edit.apply()
    }

}