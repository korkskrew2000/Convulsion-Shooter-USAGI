using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{

	#region Public
	public GunType gunType;
	public GameObject bulletImpact;
	public int bulletsLeft;
	public GameObject[] weaponGraphics;
	public Animator[] anim;
	public AudioClip[] firingSound;
	[Space(5)]
	public GameObject meleeHurtBox;
	public Animator meleeAnim;
	public bool canMelee = true;
	public int lungRemove = 5;
	public float meleeChargeTimer = 100f;
	public bool isShooting;
	public WeaponsPanel weaponsPanel;
	#endregion

	#region Private
	private int damage;
	private float timeBetweenShooting;
	private float spread;
	private int bulletsBeforeSpread = 3;
	private bool allowButtonHold;
	private int bulletsPerPress;
	private int bulletsToBeShot;
	private bool useTrail;
	private float bulletTrailTime = 1f;
	[SerializeField] private TrailRenderer bulletTrail;
	[SerializeField] private Transform trailSpawn;
	[HideInInspector] public Animator weaponAnimator;
	[HideInInspector] public AudioSource aSource;
	public bool shooting, readyToShoot;
	private Camera cam;
	private RaycastHit rayHit;
	private LayerMask ignoreMask = 1 << 8;
	public int bulletsShot;
	private bool holdButton;
	#endregion

	public enum GunType
	{
		Pistol,
		Shotgun,
		Tommygun,
		Minigun,
		RocketLauncher,
		Lazer,
		FlameThrower,
		BFG
	}

	private void Awake()
	{
		readyToShoot = true;
	}

	private void Start()
	{
		StartCoroutine(BulletFix());
		cam = Camera.main;
		aSource = this.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Player.instance.reload)
		{
			CheckGun();
		}

		if (canMelee == false)
		{
			meleeChargeTimer -= Time.deltaTime * 10;
			if (meleeChargeTimer < Player.instance.lungs)
			{
				canMelee = true;
				meleeChargeTimer = 100f;
			}
		}
		Inputting();
		Debug.DrawRay(transform.position, transform.forward * 200f, Color.red);
	}

	private void Inputting()
	{
		if (allowButtonHold) shooting = Input.GetButton("Shoot");
		else shooting = Input.GetButtonDown("Shoot");

		if (readyToShoot && shooting && bulletsLeft > 0 && !weaponsPanel.isSwitching)
		{
			Shoot();
		}

		if (allowButtonHold && shooting)
		{
			holdButton = true;
		}
		else
		{
			holdButton = false;
		}

		if (!holdButton)
		{
			bulletsShot = 0;
		}
	}

	private IEnumerator BulletFix()
	{
		yield return new WaitForSeconds(1);
		Player.instance.reload = true;
		gunType = GunType.Pistol;
		bulletsLeft = Player.instance.pistolBull;
		CheckGun();
	}

	public void Melee()
	{
		canMelee = false;
		meleeHurtBox.SetActive(true);
		meleeAnim.SetTrigger("melee");
		Invoke(nameof(ResetMelee), 0.5f);
		Player.instance.lungs -= lungRemove;
	}

	private void ResetMelee()
	{
		meleeHurtBox.SetActive(false);
	}

	private void Shoot()
	{
		aSource.pitch = Random.Range(0.9f, 1.1f);
		aSource.PlayOneShot(aSource.clip);
		readyToShoot = false;
		isShooting = true;
		weaponAnimator.SetTrigger("Shoot");
		Vector3 shootDirection = cam.transform.forward;
		while (bulletsToBeShot < bulletsPerPress)
		{
			if (bulletsShot >= bulletsBeforeSpread)
			{
				shootDirection.x += Random.Range(-spread, spread);
			}

			if (Physics.Raycast(cam.transform.position, shootDirection, out rayHit, 200f, ~ignoreMask))
			{
				Instantiate(bulletImpact, rayHit.point, Quaternion.FromToRotation(Vector3.up, rayHit.normal));
				if (useTrail)
				{
					TrailRenderer trail = Instantiate(bulletTrail, trailSpawn.transform.position, Quaternion.identity);
					StartCoroutine(SpawnTrail(trail, rayHit));
				}
				Debug.Log(rayHit.transform.name);
				if (rayHit.collider.CompareTag("Enemy"))
				{
					rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
					rayHit.collider.GetComponent<Enemy>().BloodEffect(rayHit);
				}
			}
			bulletsToBeShot++;
		}
		CheckGun();
		Invoke(nameof(ResetGun), timeBetweenShooting);
	}

	private void ResetGun()
	{
		isShooting = false;
		readyToShoot = true;
		bulletsToBeShot = 0;
		weaponAnimator.ResetTrigger("Shoot");
		weaponAnimator.Rebind();
		weaponAnimator.Update(0f);
	}

	private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
	{
		float time = 0;
		Vector3 startPosition = Trail.transform.position;
		while (time < bulletTrailTime)
		{
			Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
			time += Time.deltaTime / Trail.time;
			yield return null;
		}
		Trail.transform.position = hit.point;
		Destroy(Trail.gameObject, Trail.time);
	}

	private void CheckGun()
	{
		ButtonHold();
		bulletsLeft--;
		//there's probably a million better ways to do this but whatever
		switch (gunType)
		{
			case GunType.Pistol:
				if (Player.instance.reload) bulletsLeft = Player.instance.pistolBull;
				else Player.instance.pistolBull--;
				Player.instance.reload = false;
				/////////////////////////////////////////////////////////////////////
				damage = 15;
				timeBetweenShooting = 0.6f;
				bulletsBeforeSpread = 0;
				spread = 0;
				allowButtonHold = false;
				bulletsPerPress = 1;
				useTrail = true;
				bulletTrailTime = 3f;
				Change(0);
				break;

			case GunType.Shotgun:
				if (Player.instance.reload) bulletsLeft = Player.instance.shotgunShell;
				else Player.instance.shotgunShell--;
				Player.instance.reload = false;
				/////////////////////////////////////////////////////////////////////
				damage = 4;
				timeBetweenShooting = 1.05f;
				bulletsBeforeSpread = 0;
				spread = 0.1f;
				allowButtonHold = false;
				bulletsPerPress = 24;
				useTrail = true;
				bulletTrailTime = 5f;
				Change(1);
				break;

			case GunType.Tommygun:
				if (Player.instance.reload) bulletsLeft = Player.instance.tommyBull;
				else Player.instance.tommyBull--;
				Player.instance.reload = false;
				/////////////////////////////////////////////////////////////////////
				damage = 20;
				timeBetweenShooting = 0.2f;
				bulletsBeforeSpread = 3;
				spread = 0.15f;
				allowButtonHold = true;
				bulletsPerPress = 1;
				useTrail = true;
				bulletTrailTime = 3f;
				Change(2);
				break;

			case GunType.Minigun:
				if (Player.instance.reload) bulletsLeft = Player.instance.minigunBull;
				else Player.instance.minigunBull--;
				Player.instance.reload = false;
				/////////////////////////////////////////////////////////////////////
				damage = 30;
				timeBetweenShooting = 0.05f;
				bulletsBeforeSpread = 0;
				spread = 0.25f;
				allowButtonHold = true;
				bulletsPerPress = 1;
				useTrail = true;
				bulletTrailTime = 3f;
				Change(3);
				break;
		}

		//the shit that makes bullets go off-center after a short while
		void ButtonHold()
		{
			bulletsShot++;
		}

		//changes the fuckin animator, graphics and firing sound
		void Change(int number)
		{
			weaponAnimator = anim[number];
			WeaponGraphic(number);
			aSource.clip = firingSound[number];
		}
		void WeaponGraphic(int number)
		{
			for (int i = 0; i < weaponGraphics.Length; i++)
			{
				if (i == number)
				{
					weaponGraphics[number].SetActive(true);
				}
				else
				{
					weaponGraphics[i].SetActive(false);
				}
			}
		}
	}
}
