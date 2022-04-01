using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
	public Button beginButton;
	public Button skipButton;
	public TMP_Text beginText;
	public GameObject settingsMenu;
	public GameObject darkObj;
	public GameObject instructionsPanel;
	private Animator anim;
	private bool notFirstStart;
	private int leveltoLoad;
	private AudioSource asou;
	public AudioClip music;
	public AudioClip darkAudio;

	private void Start()
	{
		anim = GetComponent<Animator>();
		asou = GetComponent<AudioSource>();
		anim.speed = 1;
		notFirstStart = (PlayerPrefs.GetInt("NotFirstStart") != 0);
		leveltoLoad = (PlayerPrefs.GetInt("CurrentLevel"));

		if (notFirstStart)
		{
			beginText.text = "Continue";
		}
		else
		{
			beginText.text = "Start";
		}
	}

	private void Update()
	{

	}

	public void StartGame()
	{
		if (notFirstStart)
		{
			darkObj.SetActive(true);
			asou.clip = darkAudio;
			asou.Play();
			Invoke(nameof(LoadSavedScene), 1);
		}
		if (!notFirstStart)
		{
			notFirstStart = true;
			PlayerPrefs.SetInt("NotFirstStart", (notFirstStart ? 1 : 0));
			darkObj.SetActive(true);
			asou.clip = darkAudio;
			asou.Play();
			Invoke(nameof(LoadScene), 1);
		}
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(1);
	}

	private void LoadSavedScene()
	{
		if (leveltoLoad > 1)
		{
			SceneManager.LoadScene(leveltoLoad);
		}
		else
		{
			SceneManager.LoadScene(1);
		}
	}

	public void SkipAnim()
	{
		anim.speed = 100;
	}

	public void StopAnim()
	{
		anim.speed = 1;
		skipButton.gameObject.SetActive(false);
		anim.SetBool("loop", true);
	}

	public void SettingsMenu()
	{
		settingsMenu.SetActive(true);
	}

	public void ExitSettingsMenu()
	{
		settingsMenu.SetActive(false);
	}

	public void InstructionsMenu()
	{
		instructionsPanel.SetActive(true);
	}

	public void ExitInstructionsMenu()
	{
		instructionsPanel.SetActive(false);
	}

	public void QuitButton()
	{
		Application.Quit();
	}

}
