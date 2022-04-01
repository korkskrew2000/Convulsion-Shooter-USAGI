using UnityEngine;

public class DoorDistanceOpen : MonoBehaviour
{
	private Animator anim;
	private AudioSource asou;
	public AudioClip open;
	public AudioClip close;

	private void Start()
	{
		anim = GetComponent<Animator>();
		asou = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			anim.SetBool("open", true);
				asou.clip = open;
				asou.PlayOneShot(asou.clip);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			anim.SetBool("open", true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			anim.SetBool("open", false);
			asou.clip = close;
			asou.PlayOneShot(asou.clip);

		}
	}
}
