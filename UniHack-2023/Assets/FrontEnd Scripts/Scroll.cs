using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Scroll : MonoBehaviour
{
    public RectTransform canvasRect;
    public GameObject canvas;
    public Camera cam;
    public float scrollSpeed = 10f;
    public float minYPosition, maxYPosition;
    public float minim, maxim;
    public TextOn TextOn;
    Dictionary<RectTransform, Vector3> originalPositions = new Dictionary<RectTransform, Vector3>();

    void Start()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
        RectTransform firstButton = canvasRect.GetChild(0).GetComponent<RectTransform>();
        Vector3 firstButtonWorldPos = firstButton.TransformPoint(Vector3.zero);

        // Attributing minYPosition and maxYPosition
        minYPosition = 240;
        maxYPosition = firstButtonWorldPos.y + firstButton.rect.height * 0.5f;
        foreach (Transform childTransform in canvasRect)
        {
            RectTransform childRect = childTransform.GetComponent<RectTransform>();
            originalPositions[childRect] = childRect.position;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            float scrollInput = -Input.GetTouch(0).deltaPosition.y;
            ScrollContent(scrollInput);
        }
    }

    void ScrollContent(float scrollInput)
    {
        // Apply the new Y position to each child element if movement is allowed
        IdentifyVisibleLines();
        if (maxim > maxYPosition)
        {
            if (minim < minYPosition)
            {
                foreach (Transform childTransform in canvasRect)
                {
                    // Get the RectTransform of the child element
                    RectTransform childRect = childTransform.GetComponent<RectTransform>();

                    // Calculate the new Y position based on the input and scroll speed
                    float newYPosition = childRect.position.y + scrollInput * scrollSpeed;

                    // Apply the new Y position to the child element
                    childRect.position = new Vector3(childRect.position.x, newYPosition, childRect.position.z);
                }
            }
            else
            {
                if (maxim - maxYPosition >= minim - minYPosition)
                {
                    float x = minim - minYPosition;
                    foreach (Transform childTransform in canvasRect)
                    {
                        // Get the RectTransform of the child element
                        RectTransform childRect = childTransform.GetComponent<RectTransform>();

                        // Calculate the new Y position based on the input and scroll speed
                        float newYPosition = childRect.position.y - x;

                        // Apply the new Y position to the child element
                        childRect.position = new Vector3(childRect.position.x, newYPosition, childRect.position.z);
                    }
                }
                else
                {
                    foreach (Transform childTransform in canvasRect)
                    {
                        RectTransform childRect = childTransform.GetComponent<RectTransform>();
                        if (originalPositions.ContainsKey(childRect))
                            childRect.position = originalPositions[childRect];

                    }
                }
            }
        }
        else
        {
            if (maxim < maxYPosition)
            {
                float x = maxYPosition - maxim;
                foreach (Transform childTransform in canvasRect)
                {
                    // Get the RectTransform of the child element
                    RectTransform childRect = childTransform.GetComponent<RectTransform>();

                    // Calculate the new Y position based on the input and scroll speed
                    float newYPosition = childRect.position.y + x ;

                    // Apply the new Y position to the child element
                    childRect.position = new Vector3(childRect.position.x, newYPosition, childRect.position.z);
                }
            }
            else
            {
                if(scrollInput > 0)
                {
                    foreach (Transform childTransform in canvasRect)
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
        }

    }
    void IdentifyVisibleLines()
    {
        RectTransform lastButton = canvasRect.GetChild(4).GetComponent<RectTransform>();
        RectTransform textRectInLastButton = lastButton.GetChild(1).GetComponent<RectTransform>();
        RectTransform firstButton = canvasRect.GetChild(0).GetComponent<RectTransform>();

        // Check if the TMP text is visible
        // Debug.Log(textRectInLastButton.rect.height * 1.0f);
        Vector3 lastButtonWorldPos = lastButton.TransformPoint(Vector3.zero);
        Vector3 firstButtonWorldPos = firstButton.TransformPoint(Vector3.zero);
        minim = lastButtonWorldPos.y - lastButton.rect.height * 0.5f;
        maxim = firstButtonWorldPos.y + firstButton.rect.height * 0.5f;
        if (TextOn.textVisible)
            minim -= textRectInLastButton.rect.height * 1.0f;

    }
}
