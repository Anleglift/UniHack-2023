using UnityEngine;
using TMPro;

public class SpeedCalculator : MonoBehaviour
{
    public CoordinateSender CoordinateSender;
    public float latitude1;
    public float latitude2;
    public float longitude1;
    public float longitude2;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetInitialPositions", 2f);
    }

    void GetInitialPositions()
    {
        latitude1 = CoordinateSender.lat;
        longitude1 = CoordinateSender.lon;
        latitude2 = latitude1;
        longitude2 = longitude1;
        InvokeRepeating("UpdatePositions", 5f, 30f);
    }

    void UpdatePositions()
    {
        latitude1 = CoordinateSender.lat;
        longitude1 = CoordinateSender.lon;
        double distance = CalculateEuclideanDistance(latitude1, longitude1, latitude2, longitude2);

        // Calculate speed in meters per second
        double speed = distance / 30; // Assuming 5 seconds between updates

        if (speed == 0)
        {
            text.text = "Same vicinity";
        }
        else if (speed > 0 && speed <= 10) {
            text.text = "Walking";
        }
        else if (speed > 10)
        {
            text.text = "Driving";
        }
        text.text = "Speed: " + speed.ToString() + " m/s";
        latitude2 = latitude1;
        longitude2 = longitude1;
    }

    double CalculateEuclideanDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double dLat = Mathf.Abs((float)(lat2 - lat1));
        double dLon = Mathf.Abs((float)(lon2 - lon1));
        if (dLat < 0)
        {
            dLat*=-1;
        }
        if (dLon < 0)
        {
            dLon*=-1;
        }
        double distance = Mathf.Sqrt((float)(dLat * dLat + dLon * dLon));

        return distance;
    }
}
