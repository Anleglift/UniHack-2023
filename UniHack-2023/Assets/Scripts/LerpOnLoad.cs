using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpOnLoad : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public SpriteRenderer canvasIcon;
    public SpriteRenderer Logo;
    public GameObject LoadingScrene;
    public float lerpDuration = 2.0f; // Adjust the duration as needed

    private float elapsedTime = 0f;
    private bool isLerping = false;
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        StartLerping();
    }
    void StartLerping()
    {
        // Reset variables and start lerping
        elapsedTime = 0f;
        isLerping = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isLerping)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            // Lerp the alpha value from 0 to 1
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            Color lerpedColor = canvasIcon.color;
            lerpedColor.a = Mathf.Lerp(1f,0f, t);
            canvasIcon.color = lerpedColor;

            Color lerpedColor2 = Logo.color;
            lerpedColor2.a = Mathf.Lerp(1f,0f, t);
            Logo.color = lerpedColor2;
            // Check if lerping is complete
            if (t >= 1.0f)
            {
                isLerping = false;
                LoadingScrene.SetActive(false);
                other.SetActive(true);
            }
        }
    }
}
