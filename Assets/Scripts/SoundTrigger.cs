using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public float destroyTime = 1f;
    bool touched = false;
    AudioSource asou;


    void Start()
    {
        asou = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!touched)
            {
                touched = true;
                asou.PlayOneShot(asou.clip);
                Destroy(gameObject, destroyTime);
            }
        }
    }
}
