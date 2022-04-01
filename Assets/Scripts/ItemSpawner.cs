using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject objects;
    float time;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= 15f)
		{
            Instantiate(objects, transform.position, Quaternion.identity);
            time = 0f;
		}
    }
}
