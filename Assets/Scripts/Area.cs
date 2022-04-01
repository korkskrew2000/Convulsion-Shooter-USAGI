using UnityEngine;

[System.Serializable]
public class Area
{
    public string locationName;

    [TextArea(3,10)]
    public string arrivalText;
	public Color textColor;
    [Header("<-Safe, Dangerious->")]
    [Range(0, 3)]
    public int dangerLevel;
    [Space(5)]
    public TextLog textLog;
    public LocationText locText;
    public WiFiDangerLevel wifi;
}
