using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modulo18
{
	public class EnemyAI : MonoBehaviour
	{
		public Transform Player;

		private AICharacterMotor _characterMotor;
		private AttackAnimator _attackAnimator;

		private MovementMachineState _currentMovementMachineState;
		private AttackMachineState _currentAttackMachineState;

		public event Action OnHurt;

		private void Start()
		{
			_characterMotor = GetComponent<AICharacterMotor>();
			_attackAnimator = GetComponent<AttackAnimator>();

			// Inicializar states
			_currentMovementMachineState = new MovementIdle(this, _characterMotor, Player, new Stack<MovementMachineState>());
			_currentAttackMachineState = new AttackIdle(_attackAnimator, Player);
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
		}

		private float _hurtTime;
		public float HurtDuration = 0.3f;
		public float HurtMoveDuration = 0.1f;

		private void OnTriggerEnter(Collider other)
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
}
