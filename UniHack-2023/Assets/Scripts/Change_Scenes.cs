using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scenes : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject LoadingScrene;
    public SpriteRenderer canvasIcon;
    public float lerpDuration = 2.0f; // Adjust the duration as needed

    private float elapsedTime = 0f;
    private bool isLerping = false;

    public GameObject Map_Icon;
    public GameObject Home_Icon;
    public GameObject FAQ_Icon;
    public GameObject other;
    public void Change_To_Map()
    {
        Map_Icon.SetActive(true);
        Home_Icon.SetActive(false);
        FAQ_Icon.SetActive(false);
        other.SetActive(false);
        LoadingScrene.SetActive(true);
        StartLerping();
        Invoke("Change_Scene_Map", 2f);
    }
    void Change_Scene_Map()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Change_To_FAQ()
    {
        Map_Icon.SetActive(false);
        Home_Icon.SetActive(false);
        FAQ_Icon.SetActive(true);
        other.SetActive(false);
        LoadingScrene.SetActive(true);
        StartLerping();
        Invoke("Change_Scene_FAQ", 2f);
    }
    void Change_Scene_FAQ()
    {
        SceneManager.LoadScene("Questions, FAQ");
    }
    public void Change_To_Home()
    {
        Map_Icon.SetActive(false);
        Home_Icon.SetActive(true);
        FAQ_Icon.SetActive(false);
        other.SetActive(false);
        LoadingScrene.SetActive(true);
        StartLerping();
        Invoke("Change_Scene_Home", 2f);
    }
    void Change_Scene_Home()
    {
        SceneManager.LoadScene("Main Tab");
    }
    void StartLerping()
    {
        // Reset variables and start lerping
        elapsedTime = 0f;
        isLerping = true;
    }
    private void Update()
    {
        if (isLerping)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            // Lerp the alpha value from 0 to 1
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            Color lerpedColor = canvasIcon.color;
            lerpedColor.a = Mathf.Lerp(0f, 1f, t);
            canvasIcon.color = lerpedColor;
            // Check if lerping is complete
            if (t >= 1.0f)
            {
                isLerping = false;
            }
        }
    }
}
