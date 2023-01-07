using System.Collections.Generic;
using UnityEngine;

namespace Modulo18
{
	public class Patrol : Detecting
	{
		private readonly Vector3[] _patrolPositions =
			new Vector3[]
			{
				new Vector3(-4, 0, 4),
				new Vector3(4, 0, 4),
				new Vector3(4, 0, -4),
				new Vector3(-4, 0, -4),
				new Vector3(-4, 0, 4)
			};

		private int _currentIndex = 0;

		public Patrol(EnemyAI enemyAI, AICharacterMotor aiCharacterMotor, Transform player, Stack<MovementMachineState> previousStates)
			: base(enemyAI, aiCharacterMotor, player, previousStates)
		{
			Name = MovementState.Patrol;
			_currentIndex = 0;
		}

		public override void Enter()
		{
			AICharacterMotor.MoveSpeed = 3;
			AICharacterMotor.TargetPosition = _patrolPositions[_currentIndex];

			base.Enter();
		}

		public override void Update()
		{
			if (Vector3.Distance(AICharacterMotor.transform.position, _patrolPositions[_currentIndex]) <= 0.2f)
			{
				_currentIndex++;

				if (_currentIndex >= _patrolPositions.Length)
				{
					NextState = new MovementIdle(EnemyAI, AICharacterMotor, Player, PreviousStates);
					Stage = Event.Exit;
					return;
				}

				AICharacterMotor.TargetPosition = _patrolPositions[_currentIndex];
			}
			else
			{
				base.Update();
			}
		}

		public override void Exit()
		{
			if (CanSeePlayer())
			{
				PreviousStates.Push(this);
			}

			base.Exit();
		}
	}
}
