using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.Android;

public class MapView : MonoBehaviour
{
    public string apiKey;
    public float lat = -33.85660618894087f;
    public float lon = 151.21500701957325f;
    public int zoom = 12;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public Image userMarker;
    private string url = "";
    public Sprite markerSprite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckLocationPermission());
    }
    IEnumerator CheckLocationPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return new WaitForSeconds(1);
        }
        InvokeRepeating("Update_User", 0f, 2f);
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
        Input.location.Stop();
        StartCoroutine(GetGoogleMap());
    }
    void AddUserMarker(Vector2 markerPosition)
    {
        Image newMarker = Instantiate(userMarker, markerPosition, Quaternion.identity);
        newMarker.sprite = markerSprite;  // Set the image for the user marker
        newMarker.transform.SetParent(transform);  // Set the map object as the parent
    }
    void UpdateUserMarkerPosition(Vector2 markerPosition)
    {
        // If you have an existing marker to update:
        userMarker.rectTransform.anchoredPosition = markerPosition;
    }
    IEnumerator GetGoogleMap()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&key=" + apiKey;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

}