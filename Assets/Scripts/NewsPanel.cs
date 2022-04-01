using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewsPanel : MonoBehaviour
{
    [SerializeField] private GameObject textTemplate;
    [SerializeField] private RectTransform content;
    public List<GameObject> textItems;

    void Start()
    {
        textItems = new List<GameObject>();
    }

    void Update()
    {
        
    }

	public void NewNewsArticle(string NewNewsArticle, int NewsChannel)
	{
		if (textItems.Count == 6)
		{
			GameObject temp = textItems[0];
			Destroy(temp.gameObject);
			textItems.Remove(temp);
		}

		GameObject newText = Instantiate(textTemplate);
		newText.SetActive(true);
		newText.transform.SetParent(content, false);

		newText.GetComponent<NewsPanelItem>().SetText(NewNewsArticle, NewsChannel);


		textItems.Add(newText.gameObject);
	}

}
