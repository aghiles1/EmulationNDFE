package polytech.unice.fr.smartcard.nfc

import android.os.Environment
import android.util.Log
import java.io.File
import java.io.RandomAccessFile

class NDefFile {
    private var file: File = File(Environment.getExternalStorageDirectory().path, "ndefFile")

    init {
        var success = true
        if (!this.file.exists()) success = file.mkdir()
        if (!success) {
            Log.e("NDEFFILE", "File can't be created !")
        }
    }

    fun getContent(): ByteArray {
        Log.e("NDefFile","Get Content")
        return this.file.readBytes()
    }

    fun write(bytes: ByteArray) {
        Log.e("NDefFile","Write Content")
        val raf = RandomAccessFile(this.file, "rw")
        raf.setLength(0)
        raf.close()
        this.file.writeBytes(bytes)

    }
}