using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public Slider turningSpeedSlider;
	public TMP_Text turnspeedText;
	public bool notInMenu = true;

	private void Start()
	{
		turningSpeedSlider.value = PlayerPrefs.GetFloat("turnSpeed", 75f);
		turnspeedText.SetText(PlayerPrefs.GetFloat("turnSpeed", 75f).ToString());
		if (notInMenu)
		{
			PlayerMovement.instance.UpdateRotation(turningSpeedSlider.value);
		}
		turningSpeedSlider.onValueChanged.AddListener((v) =>
		{
			turnspeedText.SetText(v.ToString());
		});
	}
	public void SetTurnSpeed()
	{
		PlayerPrefs.SetFloat("turnSpeed", turningSpeedSlider.value);
		turnspeedText.SetText(turningSpeedSlider.value.ToString());
		if (notInMenu)
		{
			PlayerMovement.instance.UpdateRotation(turningSpeedSlider.value);
		}
	}

}
