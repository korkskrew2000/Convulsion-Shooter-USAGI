using UnityEngine;
using UnityEngine.UI;

public class NewsTickerText : MonoBehaviour
{
	private float tickerWidth;
	private float pixelsPerSecond;
	private RectTransform rt;

	public Text textMessage => GetComponent<Text>();
	public float GetXPosition => rt.anchoredPosition.x;
	public float GetWidth => rt.rect.width;

	public void Initialize(float tickerWidth, float pixelsPerSecond, string message)
	{
		this.tickerWidth = tickerWidth;
		this.pixelsPerSecond = pixelsPerSecond;
		rt = GetComponent<RectTransform>();
		GetComponent<Text>().text = message;
	}

	private void Update()
	{
		rt.position += Vector3.left * pixelsPerSecond * Time.deltaTime;
		if (GetXPosition <= 0 - tickerWidth - GetWidth)
		{
			Destroy(gameObject);
		}
	}
}
