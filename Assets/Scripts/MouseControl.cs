using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseControl : MonoBehaviour
{
	public static MouseControl Instance { get; private set; }

	public float Length = 5f;
	public float clickCooldown = 1f;
	public Vector3 mousePosition;
	[HideInInspector] public Ray mouseRay;
	[Space(5)]
	[SerializeField] private GameObject mouseCursor;
	[SerializeField] private UIPointer squarePointer;
	[SerializeField] private UIMouseButtonChanger clickerPointer;
	[HideInInspector] public bool clickerObj;
	public MouseStyles mouseStyles;
	public GameObject cursorNormal, cursorClick, cursorSquare;
	public Animator anim;
	public Color normalColor;
	public Color overObjectColor;
	public bool canClick = true;

	public enum MouseStyles
	{
		Normal,
		Clicker,
		Square
	}

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void Update()
	{
		switch (mouseStyles)
		{
			case MouseStyles.Normal:
				cursorNormal.gameObject.SetActive(true);
				cursorClick.gameObject.SetActive(false);
				cursorSquare.gameObject.SetActive(false);
				break;
			case MouseStyles.Clicker:
				cursorNormal.gameObject.SetActive(false);
				cursorClick.gameObject.SetActive(true);
				cursorSquare.gameObject.SetActive(false);
				break;
			case MouseStyles.Square:
				cursorNormal.gameObject.SetActive(false);
				cursorClick.gameObject.SetActive(false);
				cursorSquare.gameObject.SetActive(true);
				break;
		}
		mousePosition = Input.mousePosition;
		mouseCursor.transform.position = mousePosition;
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (squarePointer.mouseOverGrid) mouseStyles = MouseStyles.Square;
		else if (clickerPointer.mouseOverObject || clickerObj) mouseStyles = MouseStyles.Clicker;
		else mouseStyles = MouseStyles.Normal;

		if (Input.GetMouseButtonDown(0) && canClick && !EventSystem.current.IsPointerOverGameObject())
		{
			if (Physics.Raycast(mouseRay, out RaycastHit hit, Length))
			{
				if (hit.collider.CompareTag("Clickable"))
				{
					hit.collider.gameObject.GetComponent<TextClick>().SendTextOnClick();
					canClick = false;
					Invoke(nameof(ClickReset), 1);
				}
			}
		}

		if (Input.GetMouseButtonDown(0) && squarePointer.mouseOverGrid)
		{
			anim.SetBool("press", true);
			Invoke(nameof(SquareAnimReset), 0.2f);
		}


	}

	public void ColorChange()
	{
		cursorClick.GetComponent<Image>().color = overObjectColor;
	}

	public void ColorNormal()
	{
		cursorClick.GetComponent<Image>().color = normalColor;
	}

	private void SquareAnimReset()
	{
		anim.SetBool("press", false);
	}

	private void ClickReset()
	{
		canClick = true;
	}

}
