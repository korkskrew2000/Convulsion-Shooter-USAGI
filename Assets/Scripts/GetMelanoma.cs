using UnityEngine;

public class GetMelanoma : MonoBehaviour
{
	bool used = false;
	public string message;
	public Color textColor;
	TextLog textLog;
	public Inventory inv;

	private void Start()
	{
		textLog = TextLog.instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !used)
		{
			textColor.a = 255;
			textLog.LogText(message, textColor);
			inv.GetMelanoma();
			used = true;
			inv.RefreshStuff();
			Destroy(gameObject);
		}
	}
}
