using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationTracker : MonoBehaviour
{
    public AndroidJavaObject locationService;
    public TextMeshProUGUI counterText;
    public int counter;
    void Start()
    {
        StartLocationService();
    }

    void OnDestroy()
    {
        StopLocationService();
    }

    void StartLocationService()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity2d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        locationService = new AndroidJavaObject("net.gree.GameBoosters.UniHack2023.LocationService");
        currentActivity.Call("startForegroundService", new AndroidJavaObject("android.content.Intent", currentActivity, locationService));
    }

    void StopLocationService()
    {
        if (locationService != null)
        {
            locationService.Call("stopSelf");
        }
    }
    private void Update()
    {
        locationService.Call("incrementCounter");
        GetCounterFromService();
        // Update Unity UI or any other functionality with the counter value
        counterText.text = "Counter: " + counter.ToString();
    }

    int GetCounterFromService()
    {
        if (locationService != null)
        {
            counter = locationService.Call<int>("getCounter");
        }
        return 100;
    }
}
