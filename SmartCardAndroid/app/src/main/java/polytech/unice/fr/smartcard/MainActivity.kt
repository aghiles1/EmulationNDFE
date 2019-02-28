package polytech.unice.fr.smartcard

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_main.*
import polytech.unice.fr.smartcard.nfc.NDefFile

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        ndefFile.text = NDefFile().getContent().joinToString("") { String.format("%02x", it) }
    }

}
