using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class FetchingMap : MonoBehaviour
{
    public RawImage image;
    public string url = "https://static-map-unihack2023.netlify.app/";

    void Start()
    {
        StartCoroutine(LoadMap());
    }

    IEnumerator LoadMap()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error loading map: " + www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                ApplyMapToImage(texture);
            }
        }
    }

    void ApplyMapToImage(Texture2D mapTexture)
    {
        image.texture = mapTexture;
    }
}
