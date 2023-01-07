using UnityEngine;

public class RatEat : RatMachineState
{
	private float _eatTimer;

	public RatEat(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Eat;
	}

	public override void Enter()
	{
		_eatTimer = Data.EatDuration;

		AICharacterMotor.TargetPosition = Transform.position;

		TargetCrop.IsOcupied = true;

		EnemyAI.OnHurt += EnemyAI_OnHurt;

		base.Enter();
	}

	private void EnemyAI_OnHurt()
	{
		TargetCrop.IsOcupied = false;

		NextState = new RatFlee(EnemyAI);
		Stage = Event.Exit;
		EnemyAI.OnHurt -= EnemyAI_OnHurt;
	}

	public override void Update()
	{
		_eatTimer -= Time.deltaTime;

		TargetCrop.FillAmmount = (Data.EatDuration - _eatTimer) / Data.EatDuration;

		if (_eatTimer <= 0)
		{
			if (TargetCrop != null)
			{
				GameObject.Destroy(TargetCrop.gameObject);
			}

			NextState = new RatIdle(EnemyAI);
			Stage = Event.Exit;
		}
		else
		{
			base.Update();
		}
	}

	public override void Exit()
	{
		if (TargetCrop != null)
		{
			TargetCrop.FillAmmount = 0;
		}

		base.Exit();
	}
}
