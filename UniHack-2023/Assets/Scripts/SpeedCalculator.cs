using UnityEngine;
using TMPro;
using System.Drawing;

public class SpeedCalculator : MonoBehaviour
{
    public CoordinateSender CoordinateSender;
    public float latitude1;
    public float latitude2;
    public float longitude1;
    public float longitude2;
    public TextMeshProUGUI text;
    public GameObject Standing;
    public GameObject Car;
    public GameObject Walking;
    public TextMeshProUGUI recomandation;
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
        InvokeRepeating("UpdatePositions", 2f, 5f);
    }

    void UpdatePositions()
    {
        latitude1 = CoordinateSender.lat;
        longitude1 = CoordinateSender.lon;
        double distance = CalculateEuclideanDistance(latitude1, longitude1, latitude2, longitude2);

        // Calculate speed in meters per second
        double speed = distance / 5; // Assuming 5 seconds between updates

        if (speed >= 0 && speed <= 0.5f)
        {
            text.text = "Status: Same vicinity";
            Walking.SetActive(false);
            Standing.SetActive(true);
            Car.SetActive(false) ;
            recomandation.text = "If the situation allows better go for a walk!";
        }
        else if (speed > 0.5f && speed <= 3) {
            text.text = "Status: Walking";
            Walking.SetActive(true);
            Standing.SetActive(false);
            Car.SetActive(false);
            recomandation.text = "Good job, you are helping combat our climate issues";
        }
        else if (speed > 3)
        {
            text.text = "Status: Driving";
            Walking.SetActive(false);
            Standing.SetActive(false);
            Car.SetActive(true);
            recomandation.text = "It would be better for the environment if you'd walk";
        }
        latitude2 = latitude1;
        longitude2 = longitude1;
    }

    double CalculateEuclideanDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double dLat = ((float)(lat2 - lat1));
        double dLon = ((float)(lon2 - lon1));
        double distance = Mathf.Sqrt((float)(dLat * dLat + dLon * dLon));

        return distance;
    }
}
