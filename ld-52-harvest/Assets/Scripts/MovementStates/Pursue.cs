using System.Collections.Generic;
using UnityEngine;

namespace Modulo18
{
	public class Pursue : MovementMachineState
	{
		public Pursue(EnemyAI enemyAI, AICharacterMotor aiCharacterMotor, Transform player, Stack<MovementMachineState> previousStates)
			: base(enemyAI, aiCharacterMotor, player, previousStates)
		{
			Name = MovementState.Pursue;
		}

		public override void Enter()
		{
			AICharacterMotor.TargetPosition = Player.position;
			AICharacterMotor.MoveSpeed = 5f;

			EnemyAI.OnHurt += EnemyAI_OnHurt;

			base.Enter();
		}

		private void EnemyAI_OnHurt()
		{
			NextState = new Hurt(EnemyAI, AICharacterMotor, Player, PreviousStates);
			Stage = Event.Exit;
			EnemyAI.OnHurt -= EnemyAI_OnHurt;
		}

		public override void Update()
		{
			AICharacterMotor.TargetPosition = Player.position;

			if (CanAttackPlayer())
			{
				AICharacterMotor.TargetPosition = AICharacterMotor.transform.position;
			}
			else if (!CanSeePlayer())
			{
				NextState = new MovementIdle(EnemyAI, AICharacterMotor, Player, PreviousStates);
				Stage = Event.Exit;
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
