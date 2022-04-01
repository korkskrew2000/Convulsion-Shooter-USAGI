using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
	public int levelNumber;
	bool used = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !used)
		{
			GameManager.Instance.AutoSave();
			GameManager.Instance.SaveWeapons();
			StartCoroutine(GameManager.Instance.StartFade());
			Invoke(nameof(ChangeLevel), 3);
			used = true;
		}
	}

	public void ChangeLevel()
	{
		SceneManager.LoadScene(levelNumber);
	}
}
