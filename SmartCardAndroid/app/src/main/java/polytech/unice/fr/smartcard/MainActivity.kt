package polytech.unice.fr.smartcard

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.util.Log
import kotlinx.android.synthetic.main.activity_main.*
import polytech.unice.fr.smartcard.nfc.NDefFile
import android.content.Context.ACTIVITY_SERVICE
import android.support.v4.content.ContextCompat.getSystemService
import android.app.ActivityManager
import android.content.Context
import polytech.unice.fr.smartcard.nfc.CardService
import android.content.Intent


class MainActivity : AppCompatActivity() {

    private var ndef: NDefFile = NDefFile.getInstance()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        Log.e("MainActivity", "On Create")
        setContentView(R.layout.activity_main)

        ndefFile.text = this.ndef.getContent().joinToString("") { String.format("%02x", it) }

        refresh.text = "Refresh"
        refresh.setOnClickListener {
            ndefFile.text = this.ndef.getContent().joinToString("") { String.format("%02x", it) }
        }

        startService(Intent(this, CardService::class.java))

        Log.e("test", isMyServiceRunning().toString())

    }

    private fun isMyServiceRunning(): Boolean {
        val manager = getSystemService(Context.ACTIVITY_SERVICE) as ActivityManager
        for (service in manager.getRunningServices(Integer.MAX_VALUE)) {
            if (CardService::javaClass.name == service.service.className) {
                return true
            }
        }
        return false
    }

}
