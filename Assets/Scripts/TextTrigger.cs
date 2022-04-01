using UnityEngine;

public class TextTrigger : MonoBehaviour
{
	[TextArea(5, 100)]
	public string message;
	public Color textColor;
	private bool notUsed = true;
	private TextLog textLog;

	private Collider Xcollider => this.GetComponent<Collider>();

	private void Start()
	{
		textLog = TextLog.instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && notUsed)
		{
			textColor.a = 255;
			textLog.LogText(message, textColor);
			notUsed = false;
			Invoke(nameof(DestroyObject), 1);
		}
	}

	private void DestroyObject()
	{
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(Xcollider.bounds.center, Xcollider.bounds.size);
	}
}
