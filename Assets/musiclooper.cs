using UnityEngine;

public class musiclooper : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource loop;

    void Start()
    {
        if(intro.clip != null)
		{
            intro.Play();
            loop.PlayDelayed(intro.clip.length);
        }
		else
		{
            loop.Play();
		}
    }
}
