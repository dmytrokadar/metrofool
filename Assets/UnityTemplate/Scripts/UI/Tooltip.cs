using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the tooltip visibility, formatting and functionality
/// more like cooltip B-)
/// </summary>
public class Tooltip : MonoBehaviour
{
	public TextMeshProUGUI tooltipText;
	public RectTransform background;

	private bool isFirstPosition;
	private Vector2 firstPosition;

	private static Tooltip thisTooltip;

	public static string lastText { get; private set; } = "";

	[SerializeField]
	private float tooltipPadding = 20f; // https://i.giphy.com/media/l46CyJmS9KUbokzsI/giphy.webp
	[SerializeField]
	private float textSizeRatio = 1f / 45f;
	[SerializeField]
	private float tooltipDissapearanceRatio = 1f / 20f;
	[SerializeField]
	private float tooltipWidth = 0.2f;

	[SerializeField] private bool belowCursor = true;
	[SerializeField] private bool rightOfCursor = true;
	
	[SerializeField] private Vector2 offsetFromCursor = Vector2.zero;

	/// <summary>
	/// 0 - instantly move to cursor, 1 - never move to cursor (not recommended)
	/// </summary>
	[SerializeField]
	private float lerpCoeff = 10f;

	private float preferredHeight => tooltipText.preferredHeight + tooltipPadding;
	private float preferredWidth => tooltipWidth * Screen.width;

	private void Awake()
	{
		thisTooltip = this;
		tooltipText.fontSize = Screen.height * textSizeRatio;
		HideInternal();
	}

	/// <summary>
	/// If the tooltip is visible, Update will update its position to copy the mouse's, 
	/// but also stay entirely inside the screen.
	/// </summary>
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			HideInternal();
			return;
		}
		
		// get mouse coords
		Vector3 pos = Input.mousePosition;

		// get screen size + get relative position
		float x = pos.x - Screen.width / 2.0f;
		float y = pos.y - Screen.height / 2.0f;

		// customization
		if (!rightOfCursor)
		{
			x -= preferredWidth;
		}
		
		if (belowCursor)
		{
			y -= preferredHeight;
		}

		x += offsetFromCursor.x;
		y += offsetFromCursor.y;

		// normalize, so tooltip does not get out of screen
		x = Math.Min(x, Screen.width / 2.0f - preferredWidth);
		y = Math.Min(y, Screen.height / 2.0f - tooltipText.preferredHeight - tooltipPadding); // - not +

		x = Math.Max(x, -Screen.width / 2.0f);
		y = Math.Max(y, -Screen.height / 2.0f);

		// setPosition
		SetPosition(new Vector2(x, y));
	}

	/// <summary>
	/// updates position without changing activity status
	/// </summary>
	private void SetPosition(Vector2 newPosition)
	{
		// set if not set yet
		if (isFirstPosition)
		{
			firstPosition = newPosition;
			transform.localPosition = newPosition;
			isFirstPosition = false;
		}

		transform.localPosition = Vector2.Lerp(transform.localPosition, newPosition, lerpCoeff * Time.deltaTime);

		if (Vector2.Distance(newPosition, firstPosition) > Screen.width * tooltipDissapearanceRatio)
			HideInternal();
	}

	// internal function called by Show()
	private void ShowInternal(string tooltipString)
	{
		gameObject.SetActive(true);

		lastText = tooltipString;
		tooltipText.text = tooltipString;

		background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
		RectTransform parentText = tooltipText.transform.parent.GetComponent<RectTransform>();
		parentText.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
	}

	// hides tooltip
	private void HideInternal()
	{
		transform.localPosition = new Vector2(-10000, -10000); // easiest fix to fix

		isFirstPosition = true; // reset tooltip movement pivot

		gameObject.SetActive(false);
	}

	// show descriptive toolip (onEnter at gameobject)
	/// <summary>
	/// shows tooltip at position given by SetPosition. Also updates the tooltip's text
	/// the position is then updated by Update, until its hidden again
	/// </summary>
	public static void Show(string tooltipString) { thisTooltip.ShowInternal(tooltipString); }

	/// <summary>
	/// Used when the user should see the error that occured.
	/// Same functionality as Show, but also logs the error.
	/// </summary>
	/// TODO
	public static void ShowError(string errorDescription)
	{
		thisTooltip.ShowInternal("Error: " + errorDescription);
	}

	/// <summary>
	/// Hides the tooltip.
	/// Called when the cursor moves too much, or LMB initiates any action, or when the cursor moves outside the game object the tooltip is relevant to.
	/// </summary>
	public static void Hide() { thisTooltip.HideInternal(); }
}

