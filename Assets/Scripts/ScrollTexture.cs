using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
	public float scrollX = 0.5f;
	public float scrollY = 0.5f;
	public bool uiImage = false;
	private float offsetX;
	private float offsetY;

	private void Start()
	{
		offsetX = 0f;
		offsetY = 0f;
	}

	private void Update()
	{
		if (uiImage == false)
		{
			offsetX = Time.time * scrollX;
			offsetY = Time.time * scrollY;
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
		}
		if (uiImage == true)
		{
			offsetX = Time.time * scrollX;
			offsetY = Time.time * scrollY;

			GetComponent<Image>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
		}
	}
}
