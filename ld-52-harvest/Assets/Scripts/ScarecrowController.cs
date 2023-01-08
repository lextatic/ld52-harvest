using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScarecrowController : MonoBehaviour
{
	private CharacterController _characterController;
	private Vector3 _moveDirection;
	private float _moveSpeed = 5.0f;

	[SerializeField]
	private InputActionAsset _inputActions;
	private InputAction _moveAction;
	private InputAction _attackAction;
	private InputAction _rollAction;
	private InputAction _interactAction;

	public float RollDuration = 0.3f;
	public float HurtDuration = 0.3f;
	public float HurtMoveDuration = 0.1f;
	public float AttackDuration = 0.1f;

	public float RollSpeedMultiplier = 3;
	public float HurtSpeedMultiplier = 1;
	public float AttackSpeedMultiplier = 3;

	public GameObject AttackCollider;
	public GameObject WalkView;
	public GameObject AttackView;

	public SimpleAudioEvent ShooAudioEvent;
	public SimpleAudioEvent StepAudioEvent;

	public AudioSource ShooAudioSource;
	public AudioSource StepAudioSource;

	private float _rollTime;
	private float _attackTime;
	private float _hurtTime;
	private Vector3 _facingDirection;

	private bool _closeToMast;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_moveAction = _inputActions.FindAction("Move");
		_attackAction = _inputActions.FindAction("Attack");
		_rollAction = _inputActions.FindAction("Roll");
		_interactAction = _inputActions.FindAction("Interact");

		_facingDirection = Vector3.forward;

		_closeToMast = false;

		_lastStepPosition = transform.position;
	}

	private void OnEnable()
	{
		_moveAction.Enable();
		_attackAction.Enable();
		_rollAction.Enable();
		_interactAction.Enable();
	}

	private void OnDisable()
	{
		_moveAction.Disable();
		_attackAction.Disable();
		_rollAction.Disable();
		_interactAction.Disable();
	}

	private Vector3 _lastStepPosition = Vector3.zero;
	public float StepDistance = 0.3f;

	private void Update()
	{
		if (Vector3.Distance(transform.position, _lastStepPosition) > StepDistance)
		{
			StepAudioEvent.Play(StepAudioSource);

			_lastStepPosition = transform.position;
		}

		if (_rollTime >= Time.time)
		{
			_characterController.SimpleMove(_facingDirection * _moveSpeed * RollSpeedMultiplier);
		}
		else if (_hurtTime >= Time.time)
		{
			if (_hurtTime >= Time.time + HurtDuration - HurtMoveDuration)
			{
				_characterController.SimpleMove(-_facingDirection * _moveSpeed * HurtSpeedMultiplier);
			}
		}
		else if (_attackTime >= Time.time)
		{
			_characterController.SimpleMove(_facingDirection * _moveSpeed * AttackSpeedMultiplier);
		}
		else
		{
			WalkView.SetActive(true);
			AttackView.SetActive(false);
			AttackCollider.SetActive(false);

			var inputDirection = _moveAction.ReadValue<Vector2>();
			_moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
			if (_moveDirection != Vector3.zero)
			{
				_facingDirection = _moveDirection;
			}

			_characterController.SimpleMove(_moveDirection * _moveSpeed);

			if (_rollAction.triggered)
			{
				_rollTime = Time.time + RollDuration;
			}
			else if (_attackAction.triggered)
			{
				WalkView.SetActive(false);
				AttackView.SetActive(true);

				AttackCollider.SetActive(true);
				_attackTime = Time.time + AttackDuration;

				ShooAudioEvent.Play(ShooAudioSource);
			}
			else if (_interactAction.triggered && _closeToMast)
			{
				SceneManager.LoadScene(0);
			}
		}

		transform.LookAt(transform.position + _facingDirection.normalized);
	}

	private void OnTriggerStay(Collider other)
	{
		if (_rollTime >= Time.time || _hurtTime >= Time.time || _attackTime >= Time.time)
		{
			return;
		}

		if (other.CompareTag("Mast"))
		{
			_closeToMast = true;
			return;
		}

		//transform.LookAt(other.transform.position);
		_facingDirection = (other.transform.position - transform.position).normalized;
		_facingDirection.y = 0;

		_hurtTime = Time.time + HurtDuration;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Mast"))
		{
			_closeToMast = false;
		}
	}
}