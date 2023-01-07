using UnityEngine;

public class CrowGoToFarm : CrowDetecting
{
	public CrowGoToFarm(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.GoToFarm;
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
		if (Vector3.Distance(Transform.position, AICharacterMotor.TargetPosition) <= 1f)
		{
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
