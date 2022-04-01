using UnityEngine;

public class PainTile : MonoBehaviour
{
	public int damage;
	float damageTime;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			damageTime += Time.deltaTime;
			if (damageTime >= 0.25f)
			{
				other.GetComponent<Player>().TakeDamage(damage);
				damageTime = 0f;
			}
		}
	}
}
