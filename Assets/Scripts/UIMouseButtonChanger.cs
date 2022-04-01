using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseButtonChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[HideInInspector] public bool mouseOverObject;
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOverObject = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOverObject = false;
	}
}