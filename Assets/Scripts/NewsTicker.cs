using UnityEngine;

public class NewsTicker : MonoBehaviour
{
	public NewsTickerText textPrefab;
	public float itemDuration = 3.0f;
	public string textGap = " | ";
	[Header("0 should stay empty.")]
	public string[] fillerText;
	private float width;
	private float pixelsPerSecond;
	private NewsTickerText currentText;
	private bool started = false;

	private void Start()
	{
		width = GetComponent<RectTransform>().rect.width;
		pixelsPerSecond = width / itemDuration;
		AddTickerItem(fillerText[0]);
		started = true;
	}

	private void Update()
	{
		if (currentText.GetXPosition <= -currentText.GetWidth && started)
		{
			AddTickerItem(fillerText[Random.Range(1, fillerText.Length)] + textGap);
		}
	}

	private void AddTickerItem(string message)
	{
		currentText = Instantiate(textPrefab, transform);
		currentText.Initialize(width, pixelsPerSecond, message);
	}
}
