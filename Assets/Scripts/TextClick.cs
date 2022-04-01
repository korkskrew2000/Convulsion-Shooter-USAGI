using UnityEngine;

public class TextClick : MonoBehaviour
{
	[TextArea(5, 100)]
	public string message;
	public Color textColor;
	private TextLog textLog;

	private void Start()
	{
		textLog = TextLog.instance;
	}

	public void SendTextOnClick()
	{
		textColor.a = 255;
		textLog.LogText(message, textColor);
	}
}
