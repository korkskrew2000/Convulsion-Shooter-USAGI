using UnityEngine;
using UnityEngine.UI;

public class NewsPanelRandomGen : MonoBehaviour
{
    [SerializeField] private NewsPanel newsPanel;
    [TextArea(3, 10)]
    public string[] article;
    public int newsChannel;
    public float timeBeforeNext;
    float timer;
    bool prewarm = true;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(prewarm || timer >= timeBeforeNext)
		{
            if (prewarm)
            {
                for (int i = 0; i < 5; i++)
                {
                    CreateArticle();
                }
                prewarm = false;
            }

            CreateArticle();
		}
    }

    void CreateArticle()
	{
        int channelInt = Random.Range(0, 3);
        int sendChannelNumber = channelInt;

        int articleInt = Random.Range(0, article.Length);
        string sendArticle = article[articleInt];

        newsPanel.NewNewsArticle(sendArticle, sendChannelNumber);
        timer = 0;
    }
}
