using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
	public Slider bloodSlider, bladderSlider, lungSlider, muscleSlider, coolSlider;
	public GameObject bloodRegenText, statsObj;
	public bool bloodRegen;

	private Player player;
	private int interval = 10;

	private void Start()
	{
		player = Player.instance;
	}

	private void Update()
	{
		if (statsObj.activeSelf)
		{
			if (Time.frameCount % interval == 0)
			{
				if (bloodRegen) bloodRegenText.SetActive(true);
				else bloodRegenText.SetActive(false);

				bloodSlider.value = player.blood;
				bladderSlider.value = player.bladder;
				lungSlider.value = player.lungs;
				muscleSlider.value = player.muscles;
				coolSlider.value = player.cool;
			}
		}
	}
}
