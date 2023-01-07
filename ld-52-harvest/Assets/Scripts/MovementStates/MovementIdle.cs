using System.Collections.Generic;
using UnityEngine;

namespace Modulo18
{
	public class MovementIdle : Detecting
	{
		private float _idleTimer;

		public MovementIdle(EnemyAI enemyAI, AICharacterMotor aiCharacterMotor, Transform player, Stack<MovementMachineState> previousStates)
			: base(enemyAI, aiCharacterMotor, player, previousStates)
		{
			Name = MovementState.Idle;
		}

		public override void Enter()
		{
			_idleTimer = Random.Range(3f, 5f);
			base.Enter();
		}

		public override void Update()
		{
			_idleTimer -= Time.deltaTime;

			if (_idleTimer <= 0)
			{
				if (PreviousStates.Count > 0)
				{
					NextState = PreviousStates.Pop();
				}
				else
				{
					NextState = new Patrol(EnemyAI, AICharacterMotor, Player, PreviousStates);
				}

				Stage = Event.Exit;
			}
			else
			{
				base.Update();
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}

	public class Hurt : MovementMachineState
	{
		private float _hurtTimer;
		//private float _originalMovementSpeed;

		public Hurt(EnemyAI enemyAI, AICharacterMotor aiCharacterMotor, Transform player, Stack<MovementMachineState> previousStates)
			: base(enemyAI, aiCharacterMotor, player, previousStates)
		{
			Name = MovementState.Idle;
		}

		public override void Enter()
		{
			_hurtTimer = .3f;
			//_originalMovementSpeed = AICharacterMotor.MoveSpeed;
			base.Enter();
		}

		public override void Update()
		{
			_hurtTimer -= Time.deltaTime;

			if (_hurtTimer > .2f)
			{
				AICharacterMotor.ReverseDirection = true;
				AICharacterMotor.TargetPosition = AICharacterMotor.transform.position + (AICharacterMotor.transform.position - Player.position);
				//AICharacterMotor.MoveSpeed = AICharacterMotor.MoveSpeed * 5;
			}
			else
			{
				AICharacterMotor.ReverseDirection = false;
				AICharacterMotor.TargetPosition = AICharacterMotor.transform.position;
			}

			if (_hurtTimer <= 0)
			{
				NextState = new Patrol(EnemyAI, AICharacterMotor, Player, PreviousStates);
				Stage = Event.Exit;
			}
			else
			{
				base.Update();
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
