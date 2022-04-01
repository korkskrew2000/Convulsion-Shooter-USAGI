using UnityEngine;

public class ShieldImage : MonoBehaviour
{
	public GameObject High, Medium, Low, Death;

	private void Update()
	{
		if (Player.instance.shield >= 75)
		{

			High.gameObject.SetActive(true);
			Medium.gameObject.SetActive(false);
			Low.gameObject.SetActive(false);
			Death.gameObject.SetActive(false);

		}

		if (Player.instance.shield < 75 && Player.instance.shield >= 45)
		{
			High.gameObject.SetActive(false);
			Medium.gameObject.SetActive(true);
			Low.gameObject.SetActive(false);
			Death.gameObject.SetActive(false);
		}

		if (Player.instance.shield < 45 && Player.instance.shield >= 1)
		{
			High.gameObject.SetActive(false);
			Medium.gameObject.SetActive(false);
			Low.gameObject.SetActive(true);
			Death.gameObject.SetActive(false);
		}

		if (Player.instance.shield < 1)
		{
			High.gameObject.SetActive(false);
			Medium.gameObject.SetActive(false);
			Low.gameObject.SetActive(false);
			Death.gameObject.SetActive(true);
		}
	}
}
