using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public bool shotgun;
    public bool tommy;
    public bool minigun;
    public bool rocket;
    public bool lazer;
    public bool flame;
    public bool bfg;
    public WeaponsPanel panel;
    bool touched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !touched)
        {
            if (shotgun) Player.instance.haveShotgun = true;
            if (tommy) Player.instance.haveTommygun = true;
            if (minigun) Player.instance.haveMinigun = true;
            if(panel.gameObject.activeSelf == true)
			{
                panel.UpdateWeapons();
            }
            touched = true;
        }
    }
}