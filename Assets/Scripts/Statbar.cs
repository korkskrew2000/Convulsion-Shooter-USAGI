using UnityEngine;
using UnityEngine.UI;
public class Statbar : MonoBehaviour
{
	private Slider slider;

	private void Awake()
	{
		slider = GetComponent<Slider>();
	}
	public void StatBarChange(int amount)
	{
		slider.value = amount;
	}
}