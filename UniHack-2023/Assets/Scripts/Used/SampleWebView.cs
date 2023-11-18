using System.Collections;
using UnityEngine;
#if UNITY_2018_4_OR_NEWER
using UnityEngine.Networking;
#endif
using UnityEngine.UI;

public class SampleWebView : MonoBehaviour
{
    public string Url;
    public Text status;
    WebViewObject webViewObject;

    IEnumerator Start()
    {
        // Create a new GameObject for the WebViewObject and add it to the scene
        webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();

        // Initialize the WebViewObject with callback functions
        webViewObject.Init(
            cb: (msg) =>
            {
                Debug.Log(string.Format("CallFromJS[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            err: (msg) =>
            {
                Debug.Log(string.Format("CallOnError[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            httpErr: (msg) =>
            {
                Debug.Log(string.Format("CallOnHttpError[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            started: (msg) =>
            {
                Debug.Log(string.Format("CallOnStarted[{0}]", msg));
            },
            hooked: (msg) =>
            {
                Debug.Log(string.Format("CallOnHooked[{0}]", msg));
            },
            cookies: (msg) =>
            {
                Debug.Log(string.Format("CallOnCookies[{0}]", msg));
            },
            ld: (msg) =>
            {
                Debug.Log(string.Format("CallOnLoaded[{0}]", msg));

                // JavaScript code to define window.Unity.call
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                window.location = 'unity:' + msg;
                            }
                        };
                    }
                ";

                // Evaluate the JavaScript code in the WebView
                webViewObject.EvaluateJS(js + @"Unity.call('ua=' + navigator.userAgent)");
            });

        // Set WebViewObject margins and visibility
        webViewObject.SetMargins(0, 265, 0, Screen.height / 2 + 80);
        webViewObject.SetTextZoom(100);
        webViewObject.SetVisibility(true);

        // Load the WebView content on start
        yield return StartCoroutine(LoadWebViewCoroutine());
    }

    public void RefreshWebView()
    {
        StopAllCoroutines();
        Destroy(webViewObject.gameObject);
        webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();

        // Initialize the WebViewObject with callback functions
        webViewObject.Init(
            cb: (msg) =>
            {
                Debug.Log(string.Format("CallFromJS[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            err: (msg) =>
            {
                Debug.Log(string.Format("CallOnError[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            httpErr: (msg) =>
            {
                Debug.Log(string.Format("CallOnHttpError[{0}]", msg));
                status.text = msg;
                status.GetComponent<Animation>().Play();
            },
            started: (msg) =>
            {
                Debug.Log(string.Format("CallOnStarted[{0}]", msg));
            },
            hooked: (msg) =>
            {
                Debug.Log(string.Format("CallOnHooked[{0}]", msg));
            },
            cookies: (msg) =>
            {
                Debug.Log(string.Format("CallOnCookies[{0}]", msg));
            },
            ld: (msg) =>
            {
                Debug.Log(string.Format("CallOnLoaded[{0}]", msg));

                // JavaScript code to define window.Unity.call
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                window.location = 'unity:' + msg;
                            }
                        };
                    }
                ";

                // Evaluate the JavaScript code in the WebView
                webViewObject.EvaluateJS(js + @"Unity.call('ua=' + navigator.userAgent)");
            });

        // Set WebViewObject margins and visibility
        webViewObject.SetMargins(0, 265, 0, Screen.height / 2 + 80);
        webViewObject.SetTextZoom(100);
        webViewObject.SetVisibility(true);
        // Refresh the WebView content when the button is pressed
        StartCoroutine(LoadWebViewCoroutine());
    }

    IEnumerator LoadWebViewCoroutine()
    {
        // Load the WebView content based on the URL
        if (Url.StartsWith("http"))
        {
            webViewObject.LoadURL(Url.Replace(" ", "%20"));
        }
        else
        {
            // Handle loading local content (modify as needed)
            var exts = new string[]{
                ".jpg",
                ".js",
                ".html"  // should be last
            };

            foreach (var ext in exts)
            {
                var url = Url.Replace(".html", ext);
                var src = System.IO.Path.Combine(Application.streamingAssetsPath, url);
                var dst = System.IO.Path.Combine(Application.temporaryCachePath, url);
                byte[] result = null;

                if (src.Contains("://"))
                {
                    // Load content from the web for Android
#if UNITY_2018_4_OR_NEWER
                    var unityWebRequest = UnityWebRequest.Get(src);
                    yield return unityWebRequest.SendWebRequest();
                    result = unityWebRequest.downloadHandler.data;
#else
                    var www = new WWW(src);
                    yield return www;
                    result = www.bytes;
#endif
                }
                else
                {
                    // Load local content for other platforms
                    result = System.IO.File.ReadAllBytes(src);
                }

                // Save the content to a temporary cache path
                System.IO.File.WriteAllBytes(dst, result);

                if (ext == ".html")
                {
                    // Load the HTML content
                    webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
                    break;
                }
            }
        }

        yield break;
    }
}
