using UnityEngine;

public class PissDamage : MonoBehaviour
{
    public int peeDamage;
	float damageTime;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			damageTime += Time.deltaTime;

			if(damageTime >= 0.25f)
			{
				other.GetComponent<Enemy>().TakeDamage(peeDamage);
				damageTime = 0f;
			}
		}
	}
}
