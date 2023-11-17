using UnityEngine;

public class LocationServiceController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMeshPro;
    void OnSpeedUpdate(string speed)
    {
        // This method is called from Android whenever the speed is updated
        float parsedSpeed;
        if (float.TryParse(speed, out parsedSpeed))
        {
            textMeshPro.text = parsedSpeed.ToString();
        }
    }
}
