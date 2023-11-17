using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace

public class Scroll : MonoBehaviour
{
    public Transform contentContainer;
    public GameObject canvas;
    public CanvasGroup canvasGroup;
    public float scrollSpeed = 10f;
    public float minYPosition, maxYPosition;
    public float minim, maxim;
    void Start()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        maxYPosition = canvasRect.transform.position.y + canvasRect.rect.height * 0.5f - 1.0f * (contentContainer.GetChild(0).GetComponent<Button>().transform.position.y);
        minYPosition = canvasRect.transform.position.y - canvasRect.rect.height * 0.5f;

    }
    void Update()
    {
        // Get the vertical scroll input from the mouse wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Scroll the content vertically
        ScrollContent(scrollInput);
    }

    void ScrollContent(float scrollInput)
    {
        // Apply the new Y position to each child element if movement is allowed
        if (IdentifyVisibleLines())
        {
            foreach (Transform childTransform in contentContainer)
            {
                // Get the RectTransform of the child element
                RectTransform childRect = childTransform.GetComponent<RectTransform>();

                // Calculate the new Y position based on the input and scroll speed
                float newYPosition = childRect.position.y + scrollInput * scrollSpeed;

                // Apply the new Y position to the child element
                childRect.position = new Vector3(childRect.position.x, newYPosition, childRect.position.z);
            }
        }
    }

    bool IdentifyVisibleLines()
    {
        RectTransform lastButton = contentContainer.GetChild(4).GetComponent<RectTransform>();
        RectTransform textRectInLastButton = lastButton.GetComponentsInChildren<RectTransform>()[1];
        if (canvasGroup.alpha==1)
            minim = textRectInLastButton.position.y - textRectInLastButton.rect.height * 0.5f;
        else if (canvasGroup.alpha==0)
            minim = lastButton.transform.position.y - lastButton.GetComponent<RectTransform>().rect.height * 0.5f;

        RectTransform firstButton = contentContainer.GetChild(0).GetComponent<RectTransform>();
        maxim = firstButton.transform.position.y;
        Debug.Log(maxim);
        Debug.Log(minim);
        if (minim < minYPosition || maxim > maxYPosition)
            return true;
        else
            return false;
    }

    bool IsVisible(GameObject obj)
    {
        if (canvasGroup.alpha==1)
        {
            // The UI element is considered visible
            return true;
        }
        else if (canvasGroup.alpha==0)
        {
            return false;
        }
        return false;
    }
}
