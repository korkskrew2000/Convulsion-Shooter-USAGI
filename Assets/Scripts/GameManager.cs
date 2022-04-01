using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Variables
	public bool pauseMenu;
	public bool controllable;
	public float seconds = 00;
	public float minutes = 00;
	public int enemiesKilled = 0;
	public int level = 1;
	public int blood = 0;
	public int bladder = 0;
	public int lungs = 0;
	public int nanocells = 0;
	public int muscles = 0;
	public int cool = 0;
	public int InjectorsAmount = 0;
	public Animator fadeAnimation;
	public Transform spawnPoint;
	public Vector3 checkPointpos;
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;
	public bool spawned = false;
	public bool melanoma = false;
	public bool airPurifier = false;
	public bool brainDamage = false;
	public bool alkaptonuria = false;
	public bool weapon2;
	public bool weapon3;
	public bool weapon4;
	[Range(0, 3)]
	public int buttonMode = 0;
	private Player player;
	#endregion

	#region Hidden
	[HideInInspector] public Image img;
	#endregion

	/*Saved files can be found in Registry under: 
    *HKEY_CURRENT_USER\Software\Unity\UnityEditor\[company name]\[project name]*/
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
		seconds = PlayerPrefs.GetFloat("timeSeconds", 0f);
		minutes = PlayerPrefs.GetFloat("timeMinutes", 0f);
		buttonMode = (PlayerPrefs.GetInt("ButtonMode"));
		enemiesKilled = (PlayerPrefs.GetInt("EnemiesKilled"));
		level = (PlayerPrefs.GetInt("CurrentLevel"));
		blood = (PlayerPrefs.GetInt("Blood"));
		bladder = (PlayerPrefs.GetInt("Bladder"));
		lungs = (PlayerPrefs.GetInt("Lungs"));
		nanocells = (PlayerPrefs.GetInt("Nanocells"));
		muscles = (PlayerPrefs.GetInt("Muscles"));
		cool = (PlayerPrefs.GetInt("Cool"));
		melanoma = (PlayerPrefs.GetInt("Melanoma") != 0);
		airPurifier = (PlayerPrefs.GetInt("AirPurifier") != 0);
		brainDamage = (PlayerPrefs.GetInt("BrainDamage") != 0);
		alkaptonuria = (PlayerPrefs.GetInt("Alkaptonuria") != 0);
		InjectorsAmount = (PlayerPrefs.GetInt("Injectors"));

	}

	private void OnEnable()
	{
		spawnPoint = GameObject.Find("SpawnPoint").transform;
		if (spawned)
		{
			checkPointpos = new Vector3(playerPosX, playerPosY, playerPosZ);
		}
		else
		{
			checkPointpos = spawnPoint.position;
		}
		spawnPoint.transform.position = checkPointpos;
	}

	public void Start()
	{
		level = SceneManager.GetActiveScene().buildIndex;
		player = Player.instance;
		StartCoroutine(GameManager.Instance.ExitFade());
	}

	private void Update()
	{
		//Keeps track of playtime.
		seconds += Time.unscaledDeltaTime;
		if (seconds >= 60)
		{
			minutes++;
			seconds = 00;
		}
	}

	public void ButtonModeChanger(int number)
	{
		buttonMode = number;
		PlayerPrefs.SetInt("ButtonMode", number);
	}

	public IEnumerator GamePause()
	{
		pauseMenu = true;
		controllable = false;
		Time.timeScale = 0f;
		AudioListener.pause = true;
		yield return null;
	}

	public IEnumerator GameUnpause()
	{
		pauseMenu = false;
		controllable = true;
		Time.timeScale = 1;
		AudioListener.pause = false;
		yield return null;
	}

	public IEnumerator StartFade()
	{
		fadeAnimation.SetTrigger("fadein");
		yield return new WaitForSeconds(5f);
		fadeAnimation.ResetTrigger("fadein");
	}

	public IEnumerator ExitFade()
	{
		fadeAnimation.SetTrigger("fadeout");
		yield return new WaitForSeconds(5f);
		fadeAnimation.ResetTrigger("fadeout");
	}

	public IEnumerator Melanoma()
	{
		melanoma = true;
		PlayerPrefs.SetInt("Melanoma", (melanoma ? 1 : 0));
		yield return null;
	}

	public IEnumerator AirPurifier()
	{
		airPurifier = true;
		PlayerPrefs.SetInt("AirPurifier", (airPurifier ? 1 : 0));
		yield return null;
	}

	public IEnumerator BrainDamage()
	{
		brainDamage = true;
		PlayerPrefs.SetInt("BrainDamage", (brainDamage ? 1 : 0));
		yield return null;
	}

	public IEnumerator Alkaptonuria()
	{
		alkaptonuria = true;
		PlayerPrefs.SetInt("Alkaptonuria", (alkaptonuria ? 1 : 0));
		yield return null;
	}

	public void SaveWeapons()
	{
		PlayerPrefs.SetInt("Weapon2", (player.haveShotgun ? 1 : 0));
		PlayerPrefs.SetInt("Weapon3", (player.haveTommygun ? 1 : 0));
		PlayerPrefs.SetInt("Weapon4", (player.haveMinigun ? 1 : 0));
	}

	public void AutoSave()
	{
		PlayerPrefs.SetInt("EnemiesKilled", enemiesKilled);
		PlayerPrefs.SetInt("CurrentLevel", level);
		PlayerPrefs.SetInt("Blood", player.blood);
		PlayerPrefs.SetInt("Bladder", player.bladder);
		PlayerPrefs.SetInt("Lungs", player.lungs);
		PlayerPrefs.SetInt("Nanocells", player.nanocells);
		PlayerPrefs.SetInt("Muscles", player.muscles);
		PlayerPrefs.SetInt("Cool", player.cool);
		PlayerPrefs.SetInt("Injectors", InjectorsAmount);
		PlayerPrefs.SetInt("PistolBullets", player.pistolBull);
		PlayerPrefs.SetInt("ShotgunShells", player.shotgunShell);
		PlayerPrefs.SetInt("TommygunBullets", player.tommyBull);
		PlayerPrefs.SetInt("MinigunBullets", player.minigunBull);
	}

	private void OnApplicationQuit()
	{
	}
}