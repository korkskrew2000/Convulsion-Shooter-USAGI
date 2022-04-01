using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
	public int damage;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().TakeDamage(damage);
			Destroy(gameObject);
		}
		if(other.CompareTag("Enemy"))
		{
			return;
		}
		if (other.CompareTag("Untagged"))
		{
			Destroy(gameObject);
		}
	}
}
