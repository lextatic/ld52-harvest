using UnityEngine;

public class RatMove : RatDetecting
{
	public RatMove(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Move;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.WalkSpeed;

		var rand = Random.Range(0f, 1f);

		if (rand < Data.ChanceToWalkToCenterOfMap)
		{
			AICharacterMotor.TargetPosition = new Vector3(
				Random.Range(Data.MapTopLeft.x, Data.MapBottomRight.x),
				0,
				Random.Range(Data.MapTopLeft.y, Data.MapBottomRight.y));
		}
		else
		{
			var randomDirection2D = (Random.insideUnitCircle.normalized * Random.Range(Data.MinMaxWalkDistance.x, Data.MinMaxWalkDistance.y));
			AICharacterMotor.TargetPosition = Transform.position + new Vector3(randomDirection2D.x, 0, randomDirection2D.y);
		}

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
