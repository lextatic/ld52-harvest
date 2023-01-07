using UnityEngine;

public class RatFlee : RatMachineState
{
	private float _hurtTimer;
	private Vector3 _hurtDirection;

	public RatFlee(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Flee;
	}

	public override void Enter()
	{
		_hurtTimer = Data.FleeDuration;
		AICharacterMotor.MoveSpeed = Data.RunSpeed;
		_hurtDirection = Transform.position - PlayerTransform.position;
		_hurtDirection.y = 0;
		_hurtDirection.Normalize();

		base.Enter();
	}

	public override void Update()
	{
		_hurtTimer -= Time.deltaTime;

		AICharacterMotor.TargetPosition = Transform.position + _hurtDirection;//AICharacterMotor.transform.position;
																			  //}
		if (_hurtTimer <= 0)
		{
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
