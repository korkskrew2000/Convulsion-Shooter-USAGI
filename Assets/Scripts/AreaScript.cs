using UnityEngine;

public class AreaScript : MonoBehaviour
{
	public Area area;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && this.gameObject.GetComponent<Collider>() != null)
		{
			AreaBegin(area);
		}
	}

	public void AreaBegin(Area area)
	{
		area.locText.Location(area.locationName);
		area.textLog.LogText(area.arrivalText, area.textColor);
		area.wifi.Wifi(area.dangerLevel);
	}
}