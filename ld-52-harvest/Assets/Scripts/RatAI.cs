using System;
using UnityEngine;
using static RatMachineState;

public class RatAI : MonoBehaviour
{
	public RatAIData Data = new RatAIData
	{
		VisibleDistance = 5f,
		VisibaleAngle = 30f,
		AttackDistance = 1.5f,
	};

	//private AICharacterMotor _characterMotor;
	private AttackAnimator _attackAnimator;

	private RatMachineState _currentMovementMachineState;

	public event Action OnHurt;

	[SerializeField]
	private RatState CurrentState;

	private void Start()
	{
		_currentMovementMachineState = new RatMove(this);
	}

	private void Update()
	{
		//if (_hurtTime >= Time.time)
		//{
		//	if (_hurtTime >= Time.time + HurtDuration - HurtMoveDuration)
		//	{
		//		_characterMotor.TargetPosition = transform.position - transform.forward;
		//		_characterMotor.MoveSpeed = 15;
		//		_characterMotor.ReverseDirection = true;
		//	}

		//	return;
		//}

		// Atualizar states
		_currentMovementMachineState = _currentMovementMachineState.Handle();
		//_currentAttackMachineState = _currentAttackMachineState.Handle();
		CurrentState = _currentMovementMachineState.Name;
	}

	private float _hurtTime;
	public float HurtDuration = 0.3f;
	public float HurtMoveDuration = 0.1f;

	private void OnTriggerStay(Collider other)
	{
		OnHurt?.Invoke();

		//if (_hurtTime >= Time.time)
		//{
		//	return;
		//}

		//var lookPosition = other.transform.position;
		//lookPosition.y = transform.position.y;
		//transform.LookAt(lookPosition);
		////_facingDirection = (other.transform.position - transform.position).normalized;
		////_facingDirection.y = 0;

		//_hurtTime = Time.time + HurtDuration;
	}
}
