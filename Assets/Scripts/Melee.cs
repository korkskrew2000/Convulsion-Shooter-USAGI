using UnityEngine;

public class Melee : MonoBehaviour
{
    public int meleeDamage;
	public AudioSource asou;
	private Collider Xcollider => this.GetComponent<Collider>();

	private void OnTriggerEnter(Collider other)
	{
		meleeDamage = 10 + Player.instance.muscles;
		if (other.CompareTag("Enemy"))
		{
			asou.pitch = Random.Range(0.9f, 1.1f);
			asou.PlayOneShot(asou.clip);
            other.GetComponent<Enemy>().TakeDamage(meleeDamage);
        }
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(Xcollider.bounds.center, Xcollider.bounds.size);
	}

}
