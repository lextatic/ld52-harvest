using UnityEngine;
using UnityEngine.InputSystem;

public class ScarecrowController : MonoBehaviour
{
	private CharacterController characterController;
	private Vector3 moveDirection;
	private float moveSpeed = 5.0f;

	[SerializeField]
	private InputActionAsset inputActions;
	private InputAction moveAction;
	private InputAction attackAction;
	private InputAction rollAction;

	private float _rollTime;
	private float _attackTime;
	private Vector3 _lastValidDirection;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		moveAction = inputActions.FindAction("Move");
		attackAction = inputActions.FindAction("Attack");
		rollAction = inputActions.FindAction("Roll");

		_lastValidDirection = Vector3.forward;
	}

	private void OnEnable()
	{
		moveAction.Enable();
		attackAction.Enable();
		rollAction.Enable();
	}

	private void OnDisable()
	{
		moveAction.Disable();
		attackAction.Disable();
		rollAction.Disable();
	}

	private void Update()
	{
		if (_rollTime >= Time.time)
		{
			characterController.SimpleMove(_lastValidDirection * moveSpeed * 5);
		}
		else if (_attackTime >= Time.time)
		{
			characterController.SimpleMove(_lastValidDirection * moveSpeed * 3);
		}
		else
		{
			var inputDirection = moveAction.ReadValue<Vector2>();
			moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
			if (moveDirection != Vector3.zero)
			{
				_lastValidDirection = moveDirection;
			}
			characterController.SimpleMove(moveDirection * moveSpeed);

			if (rollAction.triggered)
			{
				_rollTime = Time.time + 0.3f;
			}
			else if (attackAction.triggered)
			{
				_attackTime = Time.time + 0.1f;
			}
		}
	}
}