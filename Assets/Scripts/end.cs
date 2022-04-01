using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class end : MonoBehaviour
{
    public Animator anim;
    public Button resetButton;
    public TMP_Text killstxt;
    public TMP_Text cooltxt;
    public TMP_Text result;
    public int killNumber;
    public int coolNumber;


    void Start()
    {
        killNumber = (PlayerPrefs.GetInt("EnemiesKilled"));
        killstxt.text = killNumber.ToString();
        coolNumber = (PlayerPrefs.GetInt("Cool"));
        cooltxt.text = coolNumber.ToString();
        Invoke(nameof(changescreen), 3);
    }

    void changescreen()
	{
        anim.SetBool("done", true);
	}

    public void ResetUniverse()
	{
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

}
