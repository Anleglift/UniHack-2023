package net.gree.GameBoosters.UniHack2023;

import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.os.Build;
import android.os.IBinder;
import android.os.Binder;

public class LocationService extends Service {
    public static final String CHANNEL_ID = "exampleServiceChannel";

    @Override
    public void onCreate() {
        super.onCreate();
        createNotificationChannel();
    }

    private void createNotificationChannel() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            NotificationChannel serviceChannel = new NotificationChannel(
                    CHANNEL_ID,
                    "Example Service Channel",
                    NotificationManager.IMPORTANCE_DEFAULT
            );

            NotificationManager manager = getSystemService(NotificationManager.class);
            manager.createNotificationChannel(serviceChannel);
        }
    }

    private int counter = 0;
    private final IBinder binder = new LocalBinder();

    public class LocalBinder extends Binder {
        LocationService getService() {
            return LocationService.this;
        }
    }

    public int getCounter() {
        return counter;
    }

    public void incrementCounter() {
        counter++;
    }

    @Override
    public IBinder onBind(Intent intent) {
        return binder;
    }
}
