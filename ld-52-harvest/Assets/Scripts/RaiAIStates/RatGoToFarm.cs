using UnityEngine;

public class RatGoToFarm : RatDetecting
{
	public RatGoToFarm(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.GoToFarm;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.WalkSpeed;

		AICharacterMotor.TargetPosition = new Vector3(
			Random.Range(Data.MapTopLeft.x, Data.MapBottomRight.x),
			0,
			Random.Range(Data.MapTopLeft.y, Data.MapBottomRight.y));

		base.Enter();
	}

	public override void Update()
	{
		if (Vector3.Distance(Transform.position, AICharacterMotor.TargetPosition) <= 0.2f)
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
