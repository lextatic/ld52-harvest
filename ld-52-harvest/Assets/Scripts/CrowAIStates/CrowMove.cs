using UnityEngine;

public class CrowMove : CrowDetecting
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

	public CrowMove(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.Move;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.WalkSpeed;

		var rand = Random.Range(0f, 1f);

		if (rand < .3f)
		{
			AICharacterMotor.TargetPosition = _patrolPositions[Random.Range(0, _patrolPositions.Length)];
		}
		else
		{
			var randomDirection2D = (Random.insideUnitCircle.normalized * 5);
			AICharacterMotor.TargetPosition = Transform.position + new Vector3(randomDirection2D.x, 0, randomDirection2D.y);
		}

		base.Enter();
	}

	public override void Update()
	{
		if (Data.CrowView.transform.position.y > 0)
		{
			Data.CrowView.transform.Translate(Vector3.up * 3f * Time.deltaTime);

			if (Data.CrowView.transform.position.y > Data.MaxFlightHeight)
			{
				Data.CrowView.transform.Translate(Vector3.down * (Data.CrowView.transform.position.y - Data.MaxFlightHeight));
			}
		}

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
