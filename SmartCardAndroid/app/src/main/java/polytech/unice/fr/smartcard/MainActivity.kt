package polytech.unice.fr.smartcard

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.widget.AdapterView
import android.icu.text.AlphabeticIndex.Record
import kotlinx.android.synthetic.main.activity_main.*


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        recordList.adapter = RecordAdapter(this, MutableList(0))

        recordListView.setOnItemClickListener(object : AdapterView.OnItemClickListener {

        }
    }
}
