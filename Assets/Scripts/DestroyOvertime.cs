using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
	public float time;

	private void Update()
	{
		Destroy(gameObject, time);
	}
}
