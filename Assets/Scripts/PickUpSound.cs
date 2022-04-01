using UnityEngine;

public class PickUpSound : MonoBehaviour
{
	private AudioSource asou;
	public GameObject sprite;
	public bool moving = true;
	private bool notTouched = true;

	private void Start()
	{
		asou = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (notTouched && moving)
		{
			float y = transform.position.y + Mathf.SmoothStep(-0.3f, 0.3f, Mathf.PingPong(Time.time / 5, 1));
			sprite.transform.position = new Vector3(transform.position.x, y, transform.position.z);
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Player"))
		{
			if (notTouched)
			{
				notTouched = false;
				asou.pitch = Random.Range(0.9f, 1.1f);
				asou.PlayOneShot(asou.clip);
				Destroy(sprite);
				Invoke(nameof(DestroyThis), 1f);
			}
		}
	}

	private void DestroyThis()
	{
		Destroy(gameObject);
	}
}
