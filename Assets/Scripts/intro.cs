using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public TextLog textLog;
    [TextArea(5, 40)]
    public string[] text;
    public Color color;
    bool talking = true;
    float speechTimer;
    int arrayInt = 0;
    public GameObject enemy;

    Animator anim;
    AudioSource asou;
    bool sound = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        asou = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (talking)
        {
            speechTimer += Time.deltaTime;
            if (speechTimer >= 4)
            {
                textLog.LogText(text[arrayInt], color);
                arrayInt += 1;
                speechTimer = 0;
            }

            if (arrayInt >= text.Length)
            {
                talking = false;
            }
        }

        if(enemy == null)
		{
            if(sound == false)
			{
            asou.Play();
                sound = true;
			}
            anim.SetBool("start", true);
		}

    }

    public void ChangeLevel()
	{
        SceneManager.LoadScene(2);
    }
}
