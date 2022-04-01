using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement instance;

	#region Public
	public float currentSpeed;
	public float slowestSpeed = 5f;
	public float walkingSpeed = 10f;
	public float walkingAcceleration = 5f;
	public float gravity = 30f;
	public float runningSpeed = 15f;
	public float rotationSpeed = 50f;
	public float rotationAcceleration = 20f;
	public float rotationDecceleration = 20f;

	//movement buttons
	public MoveButton LeftButton;
	public MoveButton RightButton; 
	public MoveButton UpButton;
	public MoveButton DownButton;
	public MoveButton TurnLeft;
	public MoveButton TurnRight;
	#endregion

	#region Private
	private float rotationDeccel;
	private float rotationAccel;
	private float startRotation;
	private CharacterController controller;
	private LayerMask groundMask = 1 << 6;
	private Vector2 currentDir = Vector2.zero;
	private Vector2 currentDirVelocity = Vector2.zero;
	#endregion


	#region ReadOnly
	private readonly float minimumSpeed = 0.1f;
	private readonly float groundDistance = 0.25f;
	private readonly float controllerSlope = 45.0f;
	private readonly float moveSmoothTime = 0.1f;
	#endregion

	private Vector3 rotation = Vector3.zero;
	private Vector3 _Velocity = Vector3.zero;
	public bool isGrounded;
	//Need to be public for headbobbing to work
	[HideInInspector] public Vector2 targetDir = Vector2.zero;
	public bool isMoving;
	public bool isSprinting;
	public bool isWalking;
	public RaycastHit hitGround;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	public void Start()
	{
		controller = GetComponent<CharacterController>();
		controller.slopeLimit = controllerSlope;
		UpdateRotation(PlayerPrefs.GetFloat("turnSpeed", 50f));
	}

	private void Update()
	{
		isSprinting = Input.GetButton("Sprint");
		isWalking = Input.GetButton("Slow");
	}
	public void FixedUpdate()
	{
		if (GameManager.Instance.controllable)
		{
			//Movement using Unity's built in Character Controller
			targetDir = new Vector2(Input.GetAxisRaw("HorizontalStrafe"), Input.GetAxisRaw("Vertical"));
			targetDir.Normalize();
			currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
			Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed;
			Vector3 fall = transform.up * _Velocity.y;
			Vector3 Move = velocity + fall;

			if (RightButton.IsPressed) currentDir.x += currentSpeed * Time.deltaTime;
			if (LeftButton.IsPressed) currentDir.x -= currentSpeed * Time.deltaTime;
			if (UpButton.IsPressed) currentDir.y = velocity.y += 1f;
			if (DownButton.IsPressed) currentDir.y = velocity.y -= 1f;

			controller.Move(Move * Time.deltaTime);


			this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime);
			if (TurnLeft.IsPressed) this.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
			if (TurnRight.IsPressed) this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

			this.transform.Rotate(this.rotation);
			CheckMovement();
			CheckGround();
			HandleRun();
		}

	}

	public void UpdateRotation(float newRotationSpeed)
	{
		rotationSpeed = newRotationSpeed;
		startRotation = rotationSpeed;
		rotationAccel = rotationSpeed + rotationAcceleration;
		rotationDeccel = rotationSpeed - rotationDecceleration;
	}

	private void CheckGround()
	{
		if (Physics.CheckCapsule(controller.bounds.center, new Vector3(controller.bounds.center.x,
			controller.bounds.min.y - 0.1f, controller.bounds.center.z), groundDistance, groundMask)
			|| Physics.SphereCast(transform.position + controller.center, controller.radius, Vector3.down,
			out hitGround, 1.2f, groundMask))
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		if (!isGrounded)
		{
			_Velocity.y -= 1f;
		}
		if (_Velocity.y < -40f)
		{
			_Velocity.y = -20f;
		}
		if (this.transform.position.y <= -200)
		{
			Player.instance.PlayerDies();
		}
	}

	private void HandleRun()
	{
		if (isSprinting && !isWalking)
		{
			rotationSpeed = rotationAccel;
		}
		else if (isWalking && !isSprinting)
		{
			rotationSpeed = rotationDeccel;
		}
		else
		{
			rotationSpeed = startRotation;
		}

		if (isWalking == true)
		{
			currentSpeed = slowestSpeed;
		}
		else if (isSprinting == true)
		{
			currentSpeed = runningSpeed;
		}
		else
		{
			currentSpeed = walkingSpeed;
		}
	}

	private void CheckMovement()
	{
		if (targetDir != Vector2.zero)
		{
			isMoving = true;
			if (!isSprinting)
			{
				currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, walkingAcceleration * Time.deltaTime);
			}

		}
		else
		{
			isMoving = false;
			currentSpeed = Mathf.Lerp(currentSpeed, minimumSpeed, 5 * Time.deltaTime);
		}
	}
}