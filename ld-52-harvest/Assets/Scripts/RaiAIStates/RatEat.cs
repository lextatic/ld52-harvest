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

		if (_eatTimer <= 0)
		{
			GameObject.Destroy(TargetCrop.gameObject);
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
		base.Exit();
	}
}
