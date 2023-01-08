using UnityEngine;

public class CrowMove : CrowDetecting
{
	public CrowMove(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.Move;
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

			Data.CrowAudioEvent.Play(Data.AudioSource);
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
		if (Data.CrowView.transform.position.y < Data.MaxFlightHeight)
		{
			Data.CrowView.transform.Translate(Vector3.up * Data.TakeoffSpeed * Time.deltaTime);

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
