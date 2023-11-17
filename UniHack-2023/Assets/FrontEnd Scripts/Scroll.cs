using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public Transform contentContainer;
    public float scrollSpeed = 10f;
    public float minYPosition, maxYPosition;
    void Update()
    {
        // Get the vertical scroll input from the mouse wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        IdentifyVisibleLines();
        // Scroll the content vertically
        ScrollContent(scrollInput);
    }

    void ScrollContent(float scrollInput)
    {
        
        bool CanMove = true;
        for (int i = 0; i < contentContainer.childCount; i++)
        {
            // Get the RectTransform of the child element
            RectTransform childRect = contentContainer.GetChild(i).GetComponent<RectTransform>();
            float Position = childRect.anchoredPosition.y;
            if (!(Position < maxYPosition && Position> minYPosition))
            {
                CanMove = false;
                break;
            }
        }
        if (CanMove)
        {
            // Iterate through each child in the content container
            for (int i = 0; i < contentContainer.childCount; i++)
            {
                // Get the RectTransform of the child element
                RectTransform childRect = contentContainer.GetChild(i).GetComponent<RectTransform>();
                float newYPosition = childRect.anchoredPosition.y + scrollInput * scrollSpeed;
                // Calculate the new Y position based on the input and scroll speed
                if (newYPosition <= maxYPosition && newYPosition >= minYPosition)
                    childRect.anchoredPosition = new Vector2(childRect.anchoredPosition.x, newYPosition);
                else
                    break;
                // Apply the new Y position to the child element
            }
        }
    }
    void IdentifyVisibleLines()
    {
        
        RectTransform lastChild = contentContainer.GetChild(contentContainer.childCount - 1).GetComponent<RectTransform>();
        RectTransform childOfLastChild = lastChild.GetChild(1).GetComponent<RectTransform>();
        if (IsVisible(childOfLastChild.gameObject))
            minYPosition = childOfLastChild.anchoredPosition.y;
        else
            minYPosition = lastChild.anchoredPosition.y;

        RectTransform firstChild = contentContainer.GetChild(0).GetComponent<RectTransform>();
        RectTransform childOfFirstChild = firstChild.GetChild(1).GetComponent<RectTransform>();
        if (IsVisible(childOfFirstChild.gameObject))
            maxYPosition = childOfFirstChild.anchoredPosition.y;
        else
            maxYPosition = firstChild.anchoredPosition.y;
    }
    bool IsVisible(GameObject obj)
    {
        // Get the Renderer component of the GameObject
        Renderer renderer = obj.GetComponent<Renderer>();

        // Check if the Renderer component exists and if it's visible from the camera's perspective
        if (renderer != null && renderer.isVisible)
        {
            // Additional checks can be added if needed (e.g., occlusion culling)
            return true;
        }

        return false;
    }
}
