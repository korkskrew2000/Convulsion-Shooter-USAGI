using UnityEngine;

public class FlashingScript : MonoBehaviour
{
    public GameObject flashObj;
    public float goalTime;
    public float timer;
    bool active = true;

    void LateUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= goalTime)
		{
            active = !active;
            ActivateObj();
            timer = 0f;

        }
    }

    void ActivateObj()
	{
		if (active)
		{
            flashObj.SetActive(true);
        }

		if (!active)
		{
            flashObj.SetActive(false);
        }
	}

}
