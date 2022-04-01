using UnityEngine;

public class InventoryAdd : MonoBehaviour
{
	[SerializeField] Inventory inv;
	private bool notUsed = true;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && notUsed)
		{
			inv.AddInjector();
			notUsed = false;
			Destroy(gameObject, 1);
		}
	}
}
