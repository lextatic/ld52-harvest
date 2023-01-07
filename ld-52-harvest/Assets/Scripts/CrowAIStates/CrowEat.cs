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

		EnemyAI.OnHurt += EnemyAI_OnHurt;

		base.Enter();
	}

	private void EnemyAI_OnHurt()
	{
		NextState = new CrowFlee(EnemyAI);
		Stage = Event.Exit;
		EnemyAI.OnHurt -= EnemyAI_OnHurt;
	}

	public override void Update()
	{
		_eatTimer -= Time.deltaTime;

		if (TargetCropTransform == null)
		{
			NextState = new CrowMove(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		if (_eatTimer <= 0)
		{
			GameObject.Destroy(TargetCropTransform.gameObject);
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
