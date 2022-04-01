using UnityEngine;
public class Billboard : MonoBehaviour
{

	void Update()
	{
		Vector3 direction = (Player.instance.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20);
	}
}
