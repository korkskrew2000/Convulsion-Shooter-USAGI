using UnityEngine;

public class SendLog : MonoBehaviour
{

	[TextArea(3,10)]
	public string Text;

	public Color color;

	[SerializeField]
	private TextLog textLog;

	public void SendText()
	{
		textLog.LogText(Text, color);
	}
}