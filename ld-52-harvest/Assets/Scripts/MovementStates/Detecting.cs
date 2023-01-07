using System.Collections.Generic;
using UnityEngine;

namespace Modulo18
{
	public abstract class Detecting : MovementMachineState
	{
		public Detecting(EnemyAI enemyAI, AICharacterMotor aiCharacterMotor, Transform player, Stack<MovementMachineState> previousStates)
			: base(enemyAI, aiCharacterMotor, player, previousStates)
		{
			//
		}

		public override void Enter()
		{
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
			if (CanSeePlayer())
			{
				NextState = new Pursue(EnemyAI, AICharacterMotor, Player, PreviousStates);
				Stage = Event.Exit;
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
