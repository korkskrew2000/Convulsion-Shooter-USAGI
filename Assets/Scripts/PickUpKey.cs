using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

	bool touched = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!touched)
			{
				inventory.keyCards++;
				inventory.UpdateCards();
				touched = true;
			}
		}
	}

}
