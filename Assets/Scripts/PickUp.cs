using UnityEngine;

public class PickUp : MonoBehaviour
{
	public bool health;
	public int healthAmount;
	[Space(5)]
	public bool blood;
	public int bloodAmount;
	[Space(5)]
	public bool muscle;
	public int muscleAmount;
	[Space(5)]
	public bool shield;
	public int shieldAmount;
	[Space(5)]
	public bool ammo;
	public int pistolBull;
	public int shotgunShell;
	public int tommyBull;
	public int minigunBull;
	public int rockets;
	public int lazers;
	public int flame;
	public int bfg;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (health)
			{
				Player.instance.GetHealth(healthAmount);
			}
			if (blood)
			{
				Player.instance.GetBlood(bloodAmount);
			}
			if (muscle)
			{
				Player.instance.GetMuscle(muscleAmount);
			}
			if (shield)
			{
				Player.instance.GetShield(shieldAmount);
			}
			if (ammo)
			{
				Player.instance.pistolBull += pistolBull;
				Player.instance.shotgunShell += shotgunShell;
				Player.instance.tommyBull += tommyBull;
				Player.instance.minigunBull += minigunBull;
				Player.instance.reload = true;
			}
			this.enabled = false;
		}
	}
}
