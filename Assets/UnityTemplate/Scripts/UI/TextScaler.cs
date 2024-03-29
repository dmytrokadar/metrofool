using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Automatically force text within a given screen height ratio range.
/// Possible todo: automatically check rect transform changes?
/// </summary>
public class TextScaler : MonoBehaviour
{
    [SerializeField] [Tooltip("How much part of the screen, at MOST, should the text occupy.")]
    [Range(0, 1)]
    public float maxTextRatio = 0.025f;
    
    [SerializeField] [Tooltip("How much part of the screen, at LEAST, should the text occupy.")]
    [Range(0, 1)]
    public float minTextRatio = 0.025f;

    private TextMeshProUGUI text;
    private Canvas canvas;
    
    private bool updated;
    
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null) 
            Debug.LogError("Text is not in canvas?");

        if (!text.enableAutoSizing) 
            text.enableAutoSizing = true;
    }

    private void Start()
    {
        updated = false;
    }

    private void OnValidate()
    {
        if (maxTextRatio < minTextRatio)
        {
            float avg = (maxTextRatio + minTextRatio) / 2.0f;
            maxTextRatio = avg;
            minTextRatio = avg;
        }
    }

    private void OnEnable()
    {
        updated = false;
    }

    // Check if text is missized and if yes, resize and stop auto resize.
    private void LateUpdate()
    {
        if (updated)
            return;

        updated = true;

        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        float textSize = text.fontSize;
        text.enableAutoSizing = false;

        float maxTextSize = canvasHeight * maxTextRatio;
        float minTextSize = canvasHeight * minTextRatio;

        if (textSize > maxTextSize)
        {
            text.fontSize = maxTextSize;
        }
        else if (textSize < minTextSize)
        {
            text.fontSize = minTextSize;
        }

    }
    
}
