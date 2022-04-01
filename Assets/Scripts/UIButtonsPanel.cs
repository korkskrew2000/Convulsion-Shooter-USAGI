using UnityEngine;

public class UIButtonsPanel : MonoBehaviour
{
	public GameObject Statbars, Weapons, MoveButtons, Inventory;
	public GameObject stat, weapon, move, inv, melee;
	public GameObject statPressed, weaponPressed, movePressed, invPressed, meleePressed;
	public int buttonMode;
	AudioSource asou;
	[SerializeField] MenuScript menuscript;
	[SerializeField] Gun gun;

	private void Start()
	{
		asou = GetComponent<AudioSource>();
		buttonMode = GameManager.Instance.buttonMode;

		if (buttonMode == 0)
		{
			Movbutton();
		}
		if (buttonMode == 1)
		{
			GunButton();
		}
		if (buttonMode == 2)
		{
			InvButton();
		}
		if (buttonMode == 3)
		{
			StaButton();
		}
	}

	private void Update()
	{
		if (gun.canMelee == true)
		{
			melee.gameObject.SetActive(true);
			meleePressed.gameObject.SetActive(false);
		}
		else
		{
			melee.gameObject.SetActive(false);
		}
	}


	//ok this solution fucking sucks but whatever man
	public void Movbutton()
	{
		asou.pitch = Random.Range(0.9f, 1.1f);
		asou.PlayOneShot(asou.clip);
		GameManager.Instance.ButtonModeChanger(0);

		move.gameObject.SetActive(false);
		movePressed.gameObject.SetActive(true);
		inv.gameObject.SetActive(true);
		invPressed.gameObject.SetActive(false);
		weapon.gameObject.SetActive(true);
		weaponPressed.gameObject.SetActive(false);
		stat.gameObject.SetActive(true);
		statPressed.gameObject.SetActive(false);


		MoveButtons.gameObject.SetActive(true);
		Inventory.gameObject.SetActive(false);
		Weapons.gameObject.SetActive(false);
		Statbars.gameObject.SetActive(false);
	}

	public void InvButton()
	{
		asou.pitch = Random.Range(0.9f, 1.1f);
		asou.PlayOneShot(asou.clip);
		GameManager.Instance.ButtonModeChanger(2);

		move.gameObject.SetActive(true);
		movePressed.gameObject.SetActive(false);
		inv.gameObject.SetActive(false);
		invPressed.gameObject.SetActive(true);
		weapon.gameObject.SetActive(true);
		weaponPressed.gameObject.SetActive(false);
		stat.gameObject.SetActive(true);
		statPressed.gameObject.SetActive(false);

		MoveButtons.gameObject.SetActive(false);
		Inventory.gameObject.SetActive(true);
		Weapons.gameObject.SetActive(false);
		Statbars.gameObject.SetActive(false);
	}

	public void GunButton()
	{
		asou.pitch = Random.Range(0.9f, 1.1f);
		asou.PlayOneShot(asou.clip);
		GameManager.Instance.ButtonModeChanger(1);

		move.gameObject.SetActive(true);
		movePressed.gameObject.SetActive(false);
		inv.gameObject.SetActive(true);
		invPressed.gameObject.SetActive(false);
		weapon.gameObject.SetActive(false);
		weaponPressed.gameObject.SetActive(true);
		stat.gameObject.SetActive(true);
		statPressed.gameObject.SetActive(false);

		MoveButtons.gameObject.SetActive(false);
		Inventory.gameObject.SetActive(false);
		Weapons.gameObject.SetActive(true);
		Statbars.gameObject.SetActive(false);
	}

	public void StaButton()
	{
		asou.pitch = Random.Range(0.9f, 1.1f);
		asou.PlayOneShot(asou.clip);
		GameManager.Instance.ButtonModeChanger(3);

		move.gameObject.SetActive(true);
		movePressed.gameObject.SetActive(false);
		inv.gameObject.SetActive(true);
		invPressed.gameObject.SetActive(false);
		weapon.gameObject.SetActive(true);
		weaponPressed.gameObject.SetActive(false);
		stat.gameObject.SetActive(false);
		statPressed.gameObject.SetActive(true);

		MoveButtons.gameObject.SetActive(false);
		Inventory.gameObject.SetActive(false);
		Weapons.gameObject.SetActive(false);
		Statbars.gameObject.SetActive(true);
	}

	public void MeleeButton()
	{
		asou.pitch = Random.Range(0.9f, 1.1f);
		asou.PlayOneShot(asou.clip);
		melee.gameObject.SetActive(false);
		gun.Melee();
		meleePressed.gameObject.SetActive(true);
	}

	public void PauseButton()
	{
		menuscript.PauseGame();
	}

}
