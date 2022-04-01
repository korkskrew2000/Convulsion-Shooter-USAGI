using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsPanelItem : MonoBehaviour
{
	public TMP_Text thistitle;
	public string[] titles;
	public Image[] thisimage;
	public TMP_Text thisarticle;
	public Image sprite;
	public int newsChannel;

	public void SetText(string article, int newsChannel)
	{
		if(newsChannel == 0)
		{
			sprite.sprite = thisimage[0].sprite;
			thistitle.text = titles[0];
		}
		if (newsChannel == 1)
		{
			sprite.sprite = thisimage[1].sprite;
			thistitle.text = titles[1];
		}
		if (newsChannel == 2)
		{
			sprite.sprite = thisimage[2].sprite;
			thistitle.text = titles[2];
		}


		thisarticle.text = article;
	}
}
