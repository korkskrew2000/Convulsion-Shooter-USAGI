using UnityEngine;
using UnityEngine.UI;

public class WiFiDangerLevel : MonoBehaviour
{
	[SerializeField] private Sprite[] list;
	private Image img;

	private void Awake()
	{
		img = GetComponent<Image>();
	}

	public void Wifi(int danger)
	{
		if (danger == 0) img.sprite = list[0];
		if (danger == 1) img.sprite = list[1];
		if (danger == 2) img.sprite = list[2];
		if (danger == 3) img.sprite = list[3];
	}
}
