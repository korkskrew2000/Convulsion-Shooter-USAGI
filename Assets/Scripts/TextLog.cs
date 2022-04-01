using System.Collections.Generic;
using UnityEngine;
public class TextLog : MonoBehaviour
{
	public static TextLog instance;

	[SerializeField] private GameObject textTemplate;
	[SerializeField] private RectTransform content;
	public int MaxAmount = 20;
	public List<GameObject> textItems;
	AudioSource asou;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		textItems = new List<GameObject>();
		asou = GetComponent<AudioSource>();
	}

	public void LogText(string newTextString, Color newColor)
	{
		if (textItems.Count == MaxAmount)
		{
			GameObject temp = textItems[0];
			Destroy(temp.gameObject);
			textItems.Remove(temp);
		}

		GameObject newText = Instantiate(textTemplate);
		newText.SetActive(true);
		newText.transform.SetParent(content, false);

		newText.GetComponent<TextLogItem>().SetText(newTextString, newColor);

		asou.PlayOneShot(asou.clip);
		textItems.Add(newText.gameObject);
	}
}
