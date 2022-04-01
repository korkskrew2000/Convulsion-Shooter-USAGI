using UnityEngine;

public class GetPee : MonoBehaviour
{
	bool used = false;
	public Inventory inv;

	public void Awake()
	{
		if (Random.value > 0.5) 
		{
			this.gameObject.SetActive(false);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !used)
		{
			inv.GetAlkaptonuria();
			used = true;
			Destroy(gameObject);
		}
	}
}
