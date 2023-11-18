using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextOn : MonoBehaviour
{
    public GameObject textLayer; // Reference to the text layer under the button
    public bool textVisible = false;
    public float textHeight;
    public RectTransform textLayerRectTransform;
    public RectTransform buttonRectTransform;
    public RectTransform parentRectTransform; // Reference to the parent object containing all buttons
    void Start()
    {
        textLayerRectTransform = textLayer.GetComponent<RectTransform>();
        buttonRectTransform = GetComponent<RectTransform>();
    }

    public void ToggleTextLayer()
    {
        textVisible = !textVisible;
        textLayer.SetActive(textVisible);
        textHeight = textLayerRectTransform.rect.height;
        // Adjust the position of buttons below when the text layer is active
        if (textVisible)
            MoveButtonsDown();
        else
            ResetButtonPositions();
        // Reset the position of buttons when the text layer is hidden
    }

    private void MoveButtonsDown()
    {
        // Find the parent of all buttons
        Transform parentTransform = transform.parent;
        // Iterate through each child (button) of the parent
        for (int i = transform.GetSiblingIndex() + 2; i < parentTransform.childCount; i++)
        {
            RectTransform childRectTransform = parentTransform.GetChild(i).GetComponent<RectTransform>();
            childRectTransform.anchoredPosition -= new Vector2(0f, textHeight);
        }
    }

    private void ResetButtonPositions()
    {
        Transform parentTransform = transform.parent;
        for (int i = transform.GetSiblingIndex() + 2; i < parentTransform.childCount; i++)
        {
            RectTransform childRectTransform = parentTransform.GetChild(i).GetComponent<RectTransform>();
            childRectTransform.anchoredPosition += new Vector2(0f, textHeight);
        }
    }
}
