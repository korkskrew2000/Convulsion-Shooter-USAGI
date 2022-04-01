using UnityEngine;

public class RandomChanceAppear : MonoBehaviour
{
	[Range(1.0f, 0.1f)]
	public float chance;
	private GameObject appearItem;

	private void Start()
	{
		appearItem = this.transform.GetChild(0).gameObject;

		if (Random.value > chance)
		{
			appearItem.SetActive(true);
		}
		else
		{
			appearItem.SetActive(false);
		}
	}
}
