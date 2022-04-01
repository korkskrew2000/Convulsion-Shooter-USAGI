using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text injectorAmount;
    [SerializeField] private TMP_Text keycardsAmount;
    public GameObject cashObject, brainobj, airobj, melaobj, alkaobj;
    public int injectorsLeft;
    public int keyCards;
    public bool cashMoney = false;
    public bool brainDMG;
    public bool airPRF;
    public bool melanoma;
    public bool alkaptonuria;
    Player player;
    GameManager gm;

    void Start()
    {
        player = Player.instance;
        gm = GameManager.Instance;
        injectorsLeft = gm.InjectorsAmount;
        keycardsAmount.SetText(keyCards.ToString());
        injectorAmount.SetText(injectorsLeft.ToString());
        cashObject.SetActive(false);
        brainDMG = gm.brainDamage;
        airPRF = gm.airPurifier;
        melanoma = gm.melanoma;
        alkaptonuria = gm.alkaptonuria;

        if (brainDMG) brainobj.SetActive(true);
        else brainobj.SetActive(false);
        if (airPRF) airobj.SetActive(true);
        else airobj.SetActive(false);
        if (melanoma) melaobj.SetActive(true);
        else melaobj.SetActive(false);
        if (alkaptonuria) alkaobj.SetActive(true);
        else alkaobj.SetActive(false);

    }

    public void RefreshStuff()
	{
        if (brainDMG) brainobj.SetActive(true);
        else brainobj.SetActive(false);
        if (airPRF) airobj.SetActive(true);
        else airobj.SetActive(false);
        if (melanoma) melaobj.SetActive(true);
        else melaobj.SetActive(false);
        if (alkaptonuria) alkaobj.SetActive(true);
        else alkaobj.SetActive(false);
    }

    public void GetAirPurifier()
	{
        StartCoroutine(gm.AirPurifier());
        airPRF = gm.airPurifier;
        airobj.SetActive(true);
    }

    public void GetBrainDamage()
	{
        StartCoroutine(gm.BrainDamage());
        brainDMG = gm.brainDamage;
        brainobj.SetActive(true);
    }

    public void GetMelanoma()
	{
        StartCoroutine(gm.Melanoma());
        melanoma = gm.melanoma;
        melaobj.SetActive(true);
    }

    public void GetAlkaptonuria()
	{
        StartCoroutine(gm.Alkaptonuria());
        alkaptonuria = gm.alkaptonuria;
        alkaobj.SetActive(true);
	}

    public void UpdateCash()
	{
        cashObject.SetActive(true);
        cashMoney = true;
    }

    public void UpdateCards()
	{
        keycardsAmount.SetText(keyCards.ToString());
    }

    public void UseInjector()
	{
        if(injectorsLeft > 0)
		{
            player.GetHealth(50);
            player.blood += 15;
            injectorsLeft--;
            gm.InjectorsAmount = injectorsLeft;
            injectorAmount.SetText(injectorsLeft.ToString());
        }
    }

    public void AddInjector()
	{
        injectorsLeft++;
        gm.InjectorsAmount = injectorsLeft;
        injectorAmount.SetText(injectorsLeft.ToString());
    }
}
