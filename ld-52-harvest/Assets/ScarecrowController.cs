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

	public float RollDuration = 0.3f;
	public float HurtDuration = 0.3f;
	public float HurtMoveDuration = 0.1f;
	public float AttackDuration = 0.1f;

	public float RollSpeedMultiplier = 3;
	public float HurtSpeedMultiplier = 1;
	public float AttackSpeedMultiplier = 3;

	public GameObject AttackCollider;

	private float _rollTime;
	private float _attackTime;
	private float _hurtTime;
	private Vector3 _facingDirection;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		moveAction = inputActions.FindAction("Move");
		attackAction = inputActions.FindAction("Attack");
		rollAction = inputActions.FindAction("Roll");

		_facingDirection = Vector3.forward;
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
			characterController.SimpleMove(_facingDirection * moveSpeed * RollSpeedMultiplier);
		}
		else if (_hurtTime >= Time.time)
		{
			if (_hurtTime >= Time.time + HurtDuration - HurtMoveDuration)
			{
				characterController.SimpleMove(-_facingDirection * moveSpeed * HurtSpeedMultiplier);
			}
		}
		else if (_attackTime >= Time.time)
		{
			characterController.SimpleMove(_facingDirection * moveSpeed * AttackSpeedMultiplier);
		}
		else
		{
			AttackCollider.SetActive(false);

			var inputDirection = moveAction.ReadValue<Vector2>();
			moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
			if (moveDirection != Vector3.zero)
			{
				_facingDirection = moveDirection;
			}

			characterController.SimpleMove(moveDirection * moveSpeed);

			if (rollAction.triggered)
			{
				_rollTime = Time.time + RollDuration;
			}
			else if (attackAction.triggered)
			{
				AttackCollider.SetActive(true);
				_attackTime = Time.time + AttackDuration;
			}
		}

		transform.LookAt(transform.position + _facingDirection.normalized);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_rollTime >= Time.time || _hurtTime >= Time.time || _attackTime >= Time.time)
		{
			return;
		}

		//transform.LookAt(other.transform.position);
		_facingDirection = (other.transform.position - transform.position).normalized;
		_facingDirection.y = 0;

		_hurtTime = Time.time + HurtDuration;
	}
}