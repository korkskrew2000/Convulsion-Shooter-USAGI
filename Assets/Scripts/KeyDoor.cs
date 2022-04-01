using UnityEngine;

public class KeyDoor : MonoBehaviour
{
	public int keysNeeded;
	public Color textColor;
	private TextLog textLog;
	[SerializeField] private Inventory inventory;
	private bool entered;

	private void Start()
	{
		textLog = TextLog.instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !entered)
		{
			if (inventory.keyCards < keysNeeded)
			{
				textColor.a = 255;
				textLog.LogText("You need " + keysNeeded.ToString() +" keys to unlock this door.", textColor);
			}

			if (inventory.keyCards >= keysNeeded)
			{
				textColor.a = 255;
				textLog.LogText("The door has been unlocked.", textColor);
				Destroy(gameObject);
			}
			entered = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && entered)
		{
			entered = false;
		}
	}
}
