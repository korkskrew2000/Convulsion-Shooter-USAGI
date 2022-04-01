using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
	public GameObject gunGraphics;
	[TextArea(5, 100)]
	public string[] messagesArray;
	public GameObject deathFade;
	public Color textColor;
	public GameObject hudDeath;
	bool deathAnim = false;
	bool isdead = false;
	bool animDone = false;
	AudioSource asou;
	public AudioClip clip;

    private Player player;
	private TextLog textLog;

	private void OnEnable()
	{
		player = Player.instance;
		textLog = TextLog.instance;
		asou = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (deathAnim && !animDone)
		{
			float timeElapsed = 0;
			timeElapsed += Time.deltaTime;

			if(timeElapsed <= 3)
			{
			Vector3 fall = new Vector3(transform.position.x, -0.1f, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, fall, Time.deltaTime / 2);
			}
			else
			{
				animDone = true;
			}
		}


		if (isdead && Input.anyKey)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void Death()
	{
		asou.PlayOneShot(clip);
		hudDeath.SetActive(true);
		player.cc.enabled = false;
		deathAnim = true;
		gameObject.layer = 8;
		GetComponent<PlayerMovement>().enabled = false;
		gunGraphics.SetActive(false);

		Invoke(nameof(Fade), 1);
		Invoke(nameof(DeathMessage), 2.5f);
		Invoke(nameof(RestartFunction), 4);

	}

	void DeathMessage()
	{
	
		string message = messagesArray[Random.Range(0, messagesArray.Length)];
		textColor.a = 255;
		textLog.LogText(message, textColor);
		return;
	}

	void Fade()
	{
		deathFade.SetActive(true);
	}

	void RestartFunction()
	{
		isdead = true;
		string restartMessage = "Press any key to Restart";
		textColor.a = 255;
		textLog.LogText(restartMessage, Color.white);

		if (Input.anyKey)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
