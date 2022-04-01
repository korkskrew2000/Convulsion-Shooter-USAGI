using TMPro;
using UnityEngine;

public class TextLogItem : MonoBehaviour
{

	public void SetText(string text, Color color)
	{
		GetComponent<TMP_Text>().text = text;
		GetComponent<TMP_Text>().color = color;
	}
}
