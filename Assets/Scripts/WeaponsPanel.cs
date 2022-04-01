using TMPro;
using UnityEngine;

public class WeaponsPanel : MonoBehaviour
{
	public static WeaponsPanel instance;

	public GameObject pistolButton, shotgunButton, tommyButton, miniButton;
	public TMP_Text pistol, shotgun, tommy, mini;
	private Player player;
	public Animator switchGunAnim;
	public bool isSwitching;
	[SerializeField] private Gun gun;
	bool updateW;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		player = Player.instance;
		UpdateWeapons();
	}

	private void OnEnable()
	{
		UpdateWeapons();
	}

	private void Update()
	{
		if (gameObject.activeSelf)
		{
			pistol.SetText(player.pistolBull.ToString());
			shotgun.SetText(player.shotgunShell.ToString());
			tommy.SetText(player.tommyBull.ToString());
			mini.SetText(player.minigunBull.ToString());
		}
	}

	public void UpdateWeapons()
	{
		if (player.havePistol) pistolButton.SetActive(true);
		else pistolButton.SetActive(false);
		if (player.haveShotgun) shotgunButton.SetActive(true);
		else shotgunButton.SetActive(false);
		if (player.haveTommygun) tommyButton.SetActive(true);
		else tommyButton.SetActive(false);
		if (player.haveMinigun) miniButton.SetActive(true);
		else miniButton.SetActive(false);
	}

	public void SetPistol()
	{
		if (!isSwitching && !gun.shooting && !gun.isShooting)
		{
			isSwitching = true;
			switchGunAnim.SetTrigger("switch");
			Invoke(nameof(Pistol), 0.5f);
		}
	}

	private void Pistol()
	{
		Invoke(nameof(SwitchOff), 0.5f);
		gun.gunType = Gun.GunType.Pistol;
		player.reload = true;
	}

	public void SetShotgun()
	{
		if (!isSwitching && !gun.shooting && !gun.isShooting)
		{
			isSwitching = true;
			switchGunAnim.SetTrigger("switch");
			Invoke(nameof(Shotgun), 0.5f);
		}
	}

	private void Shotgun()
	{
		Invoke(nameof(SwitchOff), 1f);
		gun.gunType = Gun.GunType.Shotgun;
		player.reload = true;
	}

	public void SetTommyGun()
	{
		if (!isSwitching && !gun.shooting && !gun.isShooting)
		{
			isSwitching = true;
			switchGunAnim.SetTrigger("switch");
			Invoke(nameof(TommyGun), 0.5f);
		}
	}

	private void TommyGun()
	{
		Invoke(nameof(SwitchOff), 0.5f);
		gun.gunType = Gun.GunType.Tommygun;
		player.reload = true;
	}

	public void SetMinigun()
	{
		if (!isSwitching && !gun.shooting && !gun.isShooting)
		{
			isSwitching = true;
			switchGunAnim.SetTrigger("switch");
			Invoke(nameof(Minigun), 0.5f);
		}
	}

	private void Minigun()
	{
		Invoke(nameof(SwitchOff), 0.5f);
		gun.gunType = Gun.GunType.Minigun;
		player.reload = true;
	}

	public void SetRocketLauncher()
	{
		gun.gunType = Gun.GunType.RocketLauncher;
		player.reload = true;
	}

	public void SetLazerGun()
	{
		gun.gunType = Gun.GunType.Lazer;
		player.reload = true;
	}

	public void SetFlameGun()
	{
		gun.gunType = Gun.GunType.FlameThrower;
		player.reload = true;
	}

	public void SetBFG()
	{
		gun.gunType = Gun.GunType.BFG;
		player.reload = true;
	}

	private void SwitchOff()
	{
		isSwitching = false;
	}
}
