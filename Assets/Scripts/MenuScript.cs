using UnityEngine;

public class MenuScript : MonoBehaviour
{
	public GameObject menuTab, settingsTab;
	bool settings;

	private void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			GameManager.Instance.pauseMenu = !GameManager.Instance.pauseMenu;
			if (GameManager.Instance.pauseMenu)
			{
				PauseGame();
			}
			if (!GameManager.Instance.pauseMenu)
			{
				UnPause();
			}
		}
	}

	public void SettingsMenu()
	{
		settings = !settings;

		if (settings)
		{
			settingsTab.gameObject.SetActive(true);
		}
		else
		{
			settingsTab.gameObject.SetActive(false);
		}
	}

	public void PauseGame()
	{
		StartCoroutine(GameManager.Instance.GamePause());
		menuTab.gameObject.SetActive(true);
		AudioListener.volume = 0.3f;
	}

	public void UnPause()
	{
		GameManager.Instance.pauseMenu = false;
		StartCoroutine(GameManager.Instance.GameUnpause());
		menuTab.gameObject.SetActive(false);
		settingsTab.gameObject.SetActive(false);
		settings = false;
		AudioListener.volume = 1.0f;
	}

	public void CloseGame()
	{
		//Doesn't work inside Unity editor.
		Application.Quit();
	}
}