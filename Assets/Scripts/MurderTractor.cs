using UnityEngine;

public class MurderTractor : MonoBehaviour
{
	public Animator tractorAnim;
	public Animator driveAround;
	public float animSpeed;
	public float drivingSpeed;

	private void Start()
	{
		tractorAnim.speed = animSpeed;
		driveAround.speed = drivingSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Player.instance.PlayerDies();
		}
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().TakeDamage(999);
		}
	}
}
