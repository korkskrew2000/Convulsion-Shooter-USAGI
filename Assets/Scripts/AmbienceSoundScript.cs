using UnityEngine;

public class AmbienceSoundScript : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip clip;
    void OnEnable()
    {
        if (clip != null)
        {
            aSource = GameObject.Find("AmbiencePlayer").GetComponent<AudioSource>();
            aSource.loop = true;
            aSource.clip = clip;
            aSource.Play();
        }
    }
}
