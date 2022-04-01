using UnityEngine;

public class MouseChangeOverObject : MonoBehaviour
{
	MouseControl mouse;

	private void Start()
	{
		mouse = MouseControl.Instance;
	}

	private void OnMouseOver()
	{
		mouse.clickerObj = true;
		mouse.ColorChange();
	}

	private void OnMouseExit()
	{
		mouse.ColorNormal();
		mouse.clickerObj = false;
	}
}
