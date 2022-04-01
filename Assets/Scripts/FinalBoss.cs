using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
	public State fightState;
	public GameObject spinObj;
	public TextLog textLog;
	[TextArea(5, 40)]
	public string[] text;
	public Color color;

	public string[] endtext;
	public Color endcolor;

	public Animator endAnimator;
	public Transform middle;
	public GameObject finalbossPrefight;
	public GameObject finalbossPhase1;
	public GameObject finalbossPhase2;

	public AudioSource asou;
	public AudioClip preFight;
	public AudioClip firstPhase;
	public AudioClip secondPhase;
	public AudioClip endPhase;

	GameObject bossObj;
	GameObject bossObj2;

	float speechTimer;
	public float endspeechTimer;
	int arrayInt = 0;
	int endarrayInt = 0;
	bool talking = true;
	Animator anim;

	bool first = true;
	bool second = true;
	bool third = true;
	public enum State
	{
		PreFight,
		FirstPhase,
		SecondPhase,
		FightEnd
	}

	private void Start()
	{
		anim = spinObj.GetComponent<Animator>();
		asou.clip = preFight;
		asou.Play();
	}

	private void Update()
	{
		switch (fightState)
		{
			case State.PreFight:
				PreFight();
				break;

			case State.FirstPhase:
				FirstPhase();
				spinObj.transform.Rotate(0, 5 * Time.deltaTime, 0);
				anim.SetBool("start", true);
				break;

			case State.SecondPhase:
				SecondPhase();
				anim.SetBool("phase2", true);

				break;

			case State.FightEnd:
				End();
				anim.SetBool("end", true);

				break;
		}
	}

	private void PreFight()
	{
		if (talking)
		{
			speechTimer += Time.deltaTime;
			if (speechTimer >= 3)
			{
				textLog.LogText(text[arrayInt], color);
				arrayInt += 1;
				speechTimer = 0;
			}

			if (arrayInt >= text.Length)
			{
				talking = false;
			}
		}

		if(finalbossPrefight == null)
		{
			fightState = State.FirstPhase;
		}
	}

	void FirstPhase()
	{
		if (first)
		{
			Instantiate(finalbossPhase1, middle.position, transform.rotation);
			asou.clip = firstPhase;
			asou.Play();
			bossObj = GameObject.Find("Enemy_FinalBoss1(Clone)");
			first = false;
		}

		if(bossObj == null)
		{
			fightState = State.SecondPhase;
		}
	}

	void SecondPhase()
	{
		if (second)
		{
			Instantiate(finalbossPhase2, middle.position, transform.rotation);
			asou.clip = secondPhase;
			asou.Play();
			bossObj2 = GameObject.Find("Enemy_FinalBoss2(Clone)");
			second = false;
		}

		if(bossObj2 == null)
		{
			fightState = State.FightEnd;
		}

	}

	void End()
	{
		
		if (third)
		{
			Player.instance.GetHealth(999);
			asou.clip = endPhase;
			asou.Play();
			Invoke(nameof(Credits), 30);
			endAnimator.SetBool("finalbossdone", true);
			third = false;
		}
		
		endspeechTimer += Time.deltaTime;
		if (endspeechTimer >= 5)
		{
			textLog.LogText(endtext[endarrayInt], endcolor);
			endarrayInt += 1;
			endspeechTimer = 0;
		}
	}

	void Credits()
	{
		SceneManager.LoadScene(11);
	}

}