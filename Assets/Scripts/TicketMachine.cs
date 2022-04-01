using UnityEngine;

public class TicketMachine : MonoBehaviour
{
	public Color textColor;
	private TextLog textLog;

	[SerializeField] private Inventory inventory;
	public GameObject gate;
	public GameObject goal;
	private bool entered;

	private void Start()
	{
		textLog = TextLog.instance;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !entered)
		{
			if (inventory.cashMoney)
			{
				textColor.a = 255;
				textLog.LogText("You shove all the money inside of the machine and forcefield vanishes!", textColor);
				RemoveGate();
			}
			else
			{
				textColor.a = 255;
				textLog.LogText("Insufficient funds.", textColor);
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


	public void RemoveGate()
	{
		gate.SetActive(false);
		goal.SetActive(true);
		Destroy(gameObject);
	}

	public void CompleteTask()
	{
		inventory.UpdateCash();
	}
}
