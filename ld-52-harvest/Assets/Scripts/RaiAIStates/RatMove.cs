using UnityEngine;

public class RatMove : Detecting
{
	private readonly Vector3[] _patrolPositions =
		new Vector3[]
		{
				new Vector3(-4, 0, 4),
				new Vector3(4, 0, 4),
				new Vector3(4, 0, -4),
				new Vector3(-4, 0, -4),
			//new Vector3(-4, 0, 4)
		};

	public RatMove(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Move;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.WalkSpeed;
		AICharacterMotor.TargetPosition = _patrolPositions[Random.Range(0, _patrolPositions.Length)];

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
