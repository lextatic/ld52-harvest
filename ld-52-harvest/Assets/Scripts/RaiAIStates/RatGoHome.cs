using UnityEngine;

public class RatGoHome : RatMachineState
{
	public RatGoHome(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.GoHome;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.RunSpeed;
		AICharacterMotor.TargetPosition = (Transform.position - Vector3.zero) * 50;

		base.Enter();
	}

	public override void Update()
	{
		base.Update();
	}

	public override void Exit()
	{
		base.Exit();
	}
}
