using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject pointer;
	[SerializeField] private GameObject ghost;
	private MouseControl mouse;
	public float ghostdelay;
	[HideInInspector] public bool mouseOverGrid = false;

	private void Start()
	{
		mouse = MouseControl.Instance;
	}

	private void Update()
	{
		if (mouseOverGrid)
		{
			pointer.gameObject.SetActive(true);
			ghost.gameObject.SetActive(true);
			pointer.transform.position = mouse.mousePosition;
			ghost.transform.position = Vector2.Lerp(ghost.transform.position, pointer.transform.position, Time.deltaTime * ghostdelay);
		}
		else
		{
			pointer.gameObject.SetActive(false);
			ghost.gameObject.SetActive(false);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOverGrid = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOverGrid = false;
	}
}