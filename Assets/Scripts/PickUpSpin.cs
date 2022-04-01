using UnityEngine;

public class PickUpSpin : MonoBehaviour
{
    public int x = 0;
    public int y = 0; 
    public int z = 50;

   void Update()
    {
        transform.Rotate(x, y, z * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
