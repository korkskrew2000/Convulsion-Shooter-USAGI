using UnityEngine;
using UnityEngine.EventSystems;

public class Piss : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private GameObject peeHurtbox;
	public ParticleSystem piss1;
	public ParticleSystem piss2;
	private Player player;
	public float temp;
	public bool pressed;
	public Color normalColor;
	public Color alkaColor;

	private void Start()
	{
		if (inventory.alkaptonuria)
		{
			ParticleSystem.MainModule sett1 = piss1.main;
			sett1.startColor = alkaColor;
			ParticleSystem.MainModule sett2 = piss2.main;
			sett2.startColor = alkaColor;
		}
		else
		{
			ParticleSystem.MainModule sett1 = piss1.main;
			sett1.startColor = normalColor;
			ParticleSystem.MainModule sett2 = piss2.main;
			sett2.startColor = normalColor;
		}
		player = Player.instance;
		
	}

	private void Update()
	{
		
		if (pressed && player.bladder != 0)
		{
			if (!piss1.isPlaying) piss1.Play();
			peeHurtbox.SetActive(true);
			temp += Time.deltaTime;
			if (temp >= 0.1f)
			{
				player.bladder -= 1;
				temp = 0;
			}
		}
		if (!pressed || player.bladder == 0)
		{
			if (piss1.isPlaying) piss1.Stop();
			peeHurtbox.SetActive(false);
		}
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		pressed = true;
	
	}

	public void OnPointerUp(PointerEventData eventData)
	{

		pressed = false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		pressed = false;
	}

}
