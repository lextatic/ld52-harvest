﻿using UnityEngine;

public class RatGoToCrop : RatMachineState
{
	public RatGoToCrop(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Move;
	}

	public override void Enter()
	{
		if (TargetCrop == null || TargetCrop.IsOcupied)
		{
			NextState = new RatMove(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		AICharacterMotor.MoveSpeed = Data.JogSpeed;
		AICharacterMotor.TargetPosition = TargetCrop.transform.position;
		AICharacterMotor.TargetPosition.y = 0;

		EnemyAI.OnHurt += EnemyAI_OnHurt;
		base.Enter();
	}

	private void EnemyAI_OnHurt()
	{
		NextState = new RatFlee(EnemyAI);
		Stage = Event.Exit;
		EnemyAI.OnHurt -= EnemyAI_OnHurt;
	}

	public override void Update()
	{
		if (TargetCrop == null || TargetCrop.IsOcupied)
		{
			NextState = new RatIdle(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		if (Vector3.Distance(Transform.position, AICharacterMotor.TargetPosition) <= 0.2f)
		{
			NextState = new RatEat(EnemyAI);
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
