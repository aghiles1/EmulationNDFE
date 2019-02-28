package polytech.unice.fr.smartcard.nfc

import android.util.Log
import polytech.unice.fr.smartcard.SmartCardApp
import java.io.File
import java.io.RandomAccessFile

class NDefFile {

    private var file: File = File(SmartCardApp.instance!!.filesDir, "ndefFile")

    init {
        this.init()
    }

    private fun init() {
        if (!this.file.createNewFile()) {
            Log.e("NDEFFILE", "File can't be created !")
        } else {
            Log.e("NDEFFILE", "File created !")
        }
    }

    fun getContent(): ByteArray {
        return if (this.file.exists()) {
            Log.e("NDefFile", "Get Content")
            this.file.readBytes()
        } else {
            byteArrayOf()
        }
    }

    fun write(bytes: ByteArray) {
        if (this.file.exists()) {
            Log.e("NDefFile", "Write Content")
            val raf = RandomAccessFile(this.file, "rw")
            raf.setLength(0)
            raf.close()
            this.file.writeBytes(bytes)
        }

    }
}