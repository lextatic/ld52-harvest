using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScarecrowController : MonoBehaviour
{
	[SerializeField]
	private InputActionAsset _inputActions;
	private InputAction _moveAction;
	private InputAction _attackAction;
	private InputAction _rollAction;
	private InputAction _interactAction;
	private InputAction _restartAction;

	public float MoveSpeed = 5.0f;

	public float RollDuration = 0.3f;
	public float AttackDuration = 0.1f;

	public float RollSpeedMultiplier = 3;
	public float AttackSpeedMultiplier = 3;

	public float StepDistance = 0.3f;

	public GameObject AttackCollider;
	public GameObject WalkView;
	public GameObject AttackView;

	public SimpleAudioEvent ShooAudioEvent;
	public SimpleAudioEvent StepAudioEvent;

	public AudioSource ShooAudioSource;
	public AudioSource StepAudioSource;

	public GameManager GameManager;

	public GameObject SHOO;

	private CharacterController _characterController;
	private Vector3 _moveDirection;

	private float _rollTime;
	private float _attackTime;
	private Vector3 _facingDirection;

	private bool _closeToMast;

	private Vector3 _lastStepPosition = Vector3.zero;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_moveAction = _inputActions.FindAction("Move");
		_attackAction = _inputActions.FindAction("Attack");
		_rollAction = _inputActions.FindAction("Roll");
		_interactAction = _inputActions.FindAction("Interact");
		_restartAction = _inputActions.FindAction("Restart");

		_facingDirection = Vector3.back;

		_closeToMast = false;

		_lastStepPosition = transform.position;

		SHOO.SetActive(false);
	}

	private void OnEnable()
	{
		_moveAction.Enable();
		_attackAction.Enable();
		_rollAction.Enable();
		_interactAction.Enable();
		_restartAction.Enable();
	}

	private void OnDisable()
	{
		_moveAction.Disable();
		_attackAction.Disable();
		_rollAction.Disable();
		_interactAction.Disable();
		_restartAction.Disable();
	}

	private void Update()
	{
		if (_restartAction.triggered)
		{
			GameManager.RestartLevel();
			return;
		}

		if (Vector3.Distance(transform.position, _lastStepPosition) > StepDistance)
		{
			StepAudioEvent.Play(StepAudioSource);

			_lastStepPosition = transform.position;
		}

		if (_rollTime >= Time.time)
		{
			_characterController.SimpleMove(_facingDirection * MoveSpeed * RollSpeedMultiplier);
		}
		else if (_attackTime >= Time.time)
		{
			_characterController.SimpleMove(_facingDirection * MoveSpeed * AttackSpeedMultiplier);
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

			_characterController.SimpleMove(_moveDirection * MoveSpeed);

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

				SHOO.SetActive(true);
				SHOO.transform.position = transform.position + (Vector3.up * 4) + (Vector3.left * 1);
				SHOO.transform.DOMove(SHOO.transform.position + (Vector3.up * 3), 0.3f).OnComplete(() =>
				{
					SHOO.SetActive(false);
				});

				ShooAudioEvent.Play(ShooAudioSource);
			}
			else if (_interactAction.triggered && _closeToMast)
			{
				GameManager.GoToNextLevel();
			}
		}

		transform.LookAt(transform.position + _facingDirection.normalized);
	}

	private void OnTriggerStay(Collider other)
	{
		if (_rollTime >= Time.time || _attackTime >= Time.time)
		{
			return;
		}

		if (other.CompareTag("Mast"))
		{
			_closeToMast = true;
			return;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Mast"))
		{
			_closeToMast = false;
		}
	}
}