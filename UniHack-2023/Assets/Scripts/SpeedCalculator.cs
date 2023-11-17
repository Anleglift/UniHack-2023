using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    public CoordinateSender CoordinateSender;
    public float latitude1;
    public float latitude2;
    public float longitude1;
    public float longitude2;
    const double EarthRadius = 6371;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetInitialPositions", 3f);
    }
    void GetInitialPositions()
    {
        latitude1=CoordinateSender.lat;
        longitude1 = CoordinateSender.lon;
        InvokeRepeating("UpdatePositions", 2f, 2f);
    }
    void UpdatePositions()
    {
        latitude2 = latitude1;
        longitude2 = longitude1;
        latitude1 = CoordinateSender.lat;
        longitude1= CoordinateSender.lon;
        
        double lat1Rad = Mathf.Deg2Rad * latitude2;
        double lat2Rad = Mathf.Deg2Rad * latitude1;
        double lon1Rad = Mathf.Deg2Rad * longitude2;
        double lon2Rad = Mathf.Deg2Rad * longitude1;

        double deltaLat = lat2Rad - lat1Rad;
        double deltaLon = lon2Rad - lon1Rad;

        double a = Mathf.Pow(Mathf.Sin((float)(deltaLat / 2)), 2) + Mathf.Cos((float)lat1Rad) * Mathf.Cos((float)lat2Rad) * Mathf.Pow(Mathf.Sin((float)(deltaLon / 2)), 2);
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1-a)));

        double distance = EarthRadius * c;

        text.text = "Speed:" + distance.ToString();
    }
}
