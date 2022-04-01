using UnityEngine;

public class AreaSong : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;
    public bool loopOnly = true;
    musiclooper ms;
    void OnEnable()
    {
        ms = GameObject.Find("MusicPlayer").GetComponent<musiclooper>();
		if (intro != null)
		{
            ms.intro.clip = intro;
            ms.loop.clip = loop;
		}
		else
		{
            ms.loop.clip = loop;
        }
    }
}
