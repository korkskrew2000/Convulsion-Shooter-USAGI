using System.Collections;
using UnityEngine;
/// <summary>
/// Moves CameraHolder up and down on moving and rotates according to player's direction.
/// </summary>
public class HeadBob : MonoBehaviour
{
	public float bobbingSpeed = 10f;
	public float bobbingAmount = 0.1f;
	public float sprintBobbingAdd = 0.05f;
	public float rotationSpeed = 2f;
	public float rotationAmountMultiplier = 2.5f;
	public float rotationResetMultiplier = 15f;
	public PlayerMovement controller;
	private float defaultPosY = 0;
	private float defaultRotZ = 0;
	private float timer = 0;
	private float rotTimer = 0;
	private Transform cameraTarget;
	private float velocityY = 0;
	private float targetY = 0;
	private float smoothTime = 0.2f;
	private Quaternion desiredRot;

	private void Start()
	{
		cameraTarget = this.transform;
		defaultPosY = transform.localPosition.y;
		defaultRotZ = transform.localRotation.z;
	}

	private void Update()
	{
		if (controller.isMoving == true && GameManager.Instance.controllable)
		{
			StartCoroutine(YChange(1.3f));
			StartCoroutine(HeadBobMove(1f));
		}
		if (controller.isMoving == false)
		{
			StartCoroutine(YChange(1.3f));
			StartCoroutine(HeadBobReset(1f));
		}

		float newY = Mathf.SmoothDamp(cameraTarget.localPosition.y, targetY, ref velocityY, smoothTime);
		cameraTarget.transform.localPosition = new Vector3(0, newY, 0);
	}

	private IEnumerator YChange(float changeAmount)
	{
		targetY = Mathf.Lerp(targetY, changeAmount, Time.deltaTime * 7.0f);
		yield return targetY;
	}

	private IEnumerator HeadBobMove(float crouchMultiplier)
	{
		timer += Time.deltaTime * bobbingSpeed / crouchMultiplier;
		if (controller.isSprinting)
		{
			timer += sprintBobbingAdd;
		}
		transform.localPosition = new Vector3(transform.localPosition.x,
			targetY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
		rotTimer += Time.deltaTime * rotationSpeed / crouchMultiplier;
		desiredRot = Quaternion.Euler(0, 0, -controller.targetDir.x * rotationAmountMultiplier);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRot, rotationSpeed * Time.deltaTime);
		yield return null;
	}

	private IEnumerator HeadBobReset(float resetSpeed)
	{
		timer = 0;
		transform.localPosition = new Vector3(transform.localPosition.x,
			Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * resetSpeed));
		rotTimer = 0;
		transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, Time.deltaTime * resetSpeed * rotationResetMultiplier);
		yield return null;
	}
}