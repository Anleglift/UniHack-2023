package net.gree.GameBoosters.UniHack2023;
import android.app.Service;
import android.content.Intent;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.IBinder;
import android.util.Log;
import com.unity3d.player.UnityPlayer;

public class LocationService extends Service {

    private LocationManager locationManager;
    private LocationListener locationListener;

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        initializeLocationManager();
    }

    private void initializeLocationManager() {
        locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);

        locationListener = new LocationListener() {
            @Override
            public void onLocationChanged(Location location) {
                float speed = location.getSpeed();
                Log.d("LocationService", "Current speed: " + speed + " m/s");
                UnityPlayer.UnitySendMessage("LocationTracker", "OnSpeedUpdate", String.valueOf(speed));
            }

            // ... (implement other location listener methods as needed)
        };

        try {
            locationManager.requestLocationUpdates(
                    LocationManager.GPS_PROVIDER,
                    5000, // 5 seconds interval
                    10,   // 10 meters distance
                    locationListener
            );
        } catch (SecurityException e) {
            Log.e("LocationService", "Error: " + e.getMessage());
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        if (locationManager != null && locationListener != null) {
            try {
                locationManager.removeUpdates(locationListener);
            } catch (SecurityException e) {
                Log.e("LocationService", "Error: " + e.getMessage());
            }
        }
    }
}
