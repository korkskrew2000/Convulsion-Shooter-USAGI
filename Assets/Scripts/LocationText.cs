using TMPro;
using UnityEngine;
public class LocationText : MonoBehaviour
{
	public TMP_Text text;

	public void Location(string locationText)
	{
		text.text = locationText;
	}
}
