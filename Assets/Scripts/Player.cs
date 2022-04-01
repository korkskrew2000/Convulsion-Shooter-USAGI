using TMPro;
using UnityEngine;
public class Player : MonoBehaviour
{
	public static Player instance;

	public int health;
	public int shield;
	bool shieldBreak;

	#region Stats
	[Header("Stats")]
	[Range(0, 100)]
	public int blood;
	[Range(0, 100)]
	public int bladder;
	[Range(0, 100)]
	public int lungs;
	[Range(0, 100)]
	public int nanocells;
	[Range(0, 100)]
	public int muscles;
	[Range(0, 100)]
	public int cool;
	[Range(0, 100)]

	#endregion

	#region Weapons
	[Header("Weapons")]
	public bool reload;
	[Space(5)]
	public bool havePistol;
	public int pistolBull;
	[Space(1)]
	int shotgun;
	public bool haveShotgun;
	public int shotgunShell;
	[Space(1)]
	int tommy;
	public bool haveTommygun;
	public int tommyBull;
	[Space(1)]
	int mini;
	public bool haveMinigun;
	public int minigunBull;
	#endregion

	#region Private
	[Space(10)]
	[SerializeField] private TMP_Text healthText;
	[SerializeField] private TMP_Text shieldText;
	[SerializeField] private Statistics stats;
	[SerializeField] private PlayerDeath playerDeath;
	[SerializeField] private Animator DamageAnim;
	private float regenTime;
	private float peeTime;
	private float breatheTime;
	AudioSource aSource;
	public AudioClip healthdmg;
	public AudioClip shielddmg;
	public AudioClip shieldbreak;
	public CharacterController cc;
	TextLog textLog;
	#endregion


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
		gameObject.layer = 3;
		cc = GetComponent<CharacterController>();
		textLog = TextLog.instance;
		aSource = GetComponent<AudioSource>();
		blood = GameManager.Instance.blood;
		bladder = GameManager.Instance.bladder;
		lungs = GameManager.Instance.lungs;
		muscles = GameManager.Instance.muscles;
		cool = GameManager.Instance.cool;

		if (health <= 50)
		{
			health = 50;
		}
		healthText.SetText(health.ToString());
		shieldText.SetText(shield.ToString());

		haveShotgun = (PlayerPrefs.GetInt("Weapon2") != 0);
		haveTommygun = (PlayerPrefs.GetInt("Weapon3") != 0);
		haveMinigun = (PlayerPrefs.GetInt("Weapon4") != 0);

		pistolBull = PlayerPrefs.GetInt("PistolBullets");
		shotgunShell = PlayerPrefs.GetInt("ShotgunShells");
		tommyBull = PlayerPrefs.GetInt("TommygunBullets");
		minigunBull = PlayerPrefs.GetInt("MinigunBullets");

		cc.enabled = false;
		transform.position = GameManager.Instance.spawnPoint.transform.position;
		cc.enabled = true;
	}

	private void Update()
	{
		HandleStats();
	}

	#region Player Stuff
	public void GetHealth(int healthAmount)
	{
		if (health <= 100)
		{
			health += healthAmount;
			healthText.SetText(health.ToString());
		}
		if (health > 100)
		{
			health = 100;
			healthText.SetText(health.ToString());
		}
	}



	public void GetShield(int shieldAmount)
	{
		nanocells += 1;
		nanocells = Mathf.Clamp(bladder, 0, 100);
		if (shield <= 100)
		{
			if (shieldBreak)
			{
				shieldBreak = false;
			}
			shield += shieldAmount;
			shieldText.SetText(shield.ToString());
		}
		if (shield > 100)
		{
			shield = 100;
			shieldText.SetText(shield.ToString());
		}
	}

	public void TakeDamage(int damage)
	{
		cool -= 5;
		if(cool < 0)
		{
			cool = 0;
		}
		if(cool > 100)
		{
			cool = 100;
		}

		if (shield > 0)
		{
			shield -= damage;
			DamageAnim.SetTrigger("shield");
			aSource.PlayOneShot(shielddmg);
			healthText.SetText(health.ToString());
			shieldText.SetText(shield.ToString());
		}
		else
		{
			health -= damage;
			DamageAnim.SetTrigger("health");
			aSource.PlayOneShot(healthdmg);
			healthText.SetText(health.ToString());
			shieldText.SetText(shield.ToString());
		}

		if (shield <= 0)
		{
			shield = 0;
			if (!shieldBreak)
			{
				shieldBreak = true;
				DamageAnim.SetTrigger("shieldbreak");
				aSource.PlayOneShot(shieldbreak);
			}
		}

		if (health <= 0)
		{
			PlayerDies();
		}


		healthText.SetText(health.ToString());
		shieldText.SetText(shield.ToString());
	}

	public void PlayerDies()
	{
		GetComponent<PlayerDeath>().enabled = true;
		playerDeath.Death();
	}
	#endregion

	#region Statistics
	private void HandleStats()
	{
		#region Counters
		if (bladder != 100) peeTime += Time.deltaTime;
		if (lungs != 100) breatheTime += Time.deltaTime;

		if (peeTime >= 5f)
		{
			bladder += 4;
			peeTime = 0;
			bladder = Mathf.Clamp(bladder, 0, 100);
		}

		if (breatheTime >= 5f)
		{
			lungs += 5;
			breatheTime = 0;
			lungs = Mathf.Clamp(lungs, 0, 100);
		}
		#endregion


		if (blood >= 30 && blood <= 49)
		{
			BloodRegeneration(5, 20);
		}
		if (blood >= 50 && blood <= 79)
		{
			BloodRegeneration(10, 10);
		}
		if (blood >= 80 && blood <= 100)
		{
			BloodRegeneration(20, 5);
		}
	}

	public void GetBlood(int bloodAmount)
	{
		blood += bloodAmount;
		if (blood < 0)
		{
			blood = 0;
		}
		if (blood > 100)
		{
			blood = 100;
		}
	}

	public void GetMuscle(int muscleAmount)
	{
		muscles += muscleAmount;
		if (muscles < 0)
		{
			muscles = 0;
		}
		if (muscles > 100)
		{
			muscles = 100;
		}
	}

	private void BloodRegeneration(int healthRegenerated, float regenTimeGoal)
	{
		if (health < 100)
		{
			stats.bloodRegen = true;
			regenTime += Time.deltaTime;
			if (regenTime >= regenTimeGoal)
			{
				GetHealth(healthRegenerated);
				regenTime = 0;
			}
		}
		else
		{
			stats.bloodRegen = false;
			return;
		}
	}
	#endregion
}
