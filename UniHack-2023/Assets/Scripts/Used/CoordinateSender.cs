using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Android;
using TMPro;

public class CoordinateSender : MonoBehaviour
{
    private const string serverUrl = "https://static-map-unihack2023.netlify.app/updateCoordinates";
    private float updateInterval = 1f; // Update interval in seconds
    public string url2;
    public SampleWebView SampleWebview;
    public float lat = -33.85660618894087f;
    public float lon = 151.21500701957325f;
    public GameObject Map;
    public TextMeshProUGUI text;
    void Start()
    {
        StartCoroutine(CheckLocationPermission());
    }
    IEnumerator CheckLocationPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            yield return new WaitForSeconds(1);
        }
        InvokeRepeating("Update_User", 0f, 5f);
    }
    void Update_User()
    {
        StartCoroutine(GetUserLocation());
    }
    IEnumerator GetUserLocation()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
        string latString = lat.ToString().Replace(',', '.');
        string lonString = lon.ToString().Replace(',', '.');
        url2 = $"https://static-map-unihack2023.netlify.app/?lat={latString}&lng={lonString}";
        Debug.Log(url2);
        SampleWebview.Url = url2;
        Invoke("Initialise_Map", 2.5f);
    }
    void Initialise_Map()
    {
        Map.SetActive(true);
    }
    // Stop location services when the script is destroyed
    void OnDestroy()
    {
        Input.location.Stop();
    }
}
