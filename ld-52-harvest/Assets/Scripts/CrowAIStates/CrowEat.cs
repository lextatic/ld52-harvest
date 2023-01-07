using UnityEngine;

public class CrowEat : CrowMachineState
{
	private float _eatTimer;

	public CrowEat(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.Eat;
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

		NextState = new CrowFlee(EnemyAI);
		Stage = Event.Exit;

		EnemyAI.OnHurt -= EnemyAI_OnHurt;
	}

	public override void Update()
	{
		_eatTimer -= Time.deltaTime;

		if (_eatTimer <= 0)
		{
			GameObject.Destroy(TargetCrop.gameObject);
			NextState = new CrowMove(EnemyAI);
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
