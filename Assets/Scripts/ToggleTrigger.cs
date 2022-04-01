using UnityEngine;

public class ToggleTrigger : MonoBehaviour
{
	private bool triggered = false;
	public GameObject[] toActivate;
	public GameObject[] toDisable;

	private void OnEnable()
	{
		triggered = false;
		if (toActivate.Length > 0)
		{
			for (int i = 0; i < toActivate.Length; i++)
			{
				toActivate[i].SetActive(false);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !triggered)
		{
			triggered = true;
			if (toActivate.Length > 0)
			{
				for (int i = 0; i < toActivate.Length; i++)
				{
					toActivate[i].SetActive(true);
				}
			}

			if (toDisable.Length > 0)
			{
				for (int i = 0; i < toDisable.Length; i++)
				{
					toDisable[i].SetActive(false);
				}
			}
			gameObject.SetActive(false);
		}
	}
}
