package polytech.unice.fr.smartcard

import android.app.Application

class SmartCardApp : Application() {

    companion object {
        var instance: SmartCardApp? = null
    }

    override fun onCreate() {
        instance = this
        super.onCreate()
    }

}