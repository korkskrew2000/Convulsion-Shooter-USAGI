using UnityEngine;

public class voidnet : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = Player.instance; 
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player.cc.enabled = false;
			player.transform.position = GameManager.Instance.spawnPoint.transform.position;
			player.cc.enabled = true;
		}
	}
}
