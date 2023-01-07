using UnityEngine;

public class CrowGoHome : CrowDetecting
{
	public CrowGoHome(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.GoHome;
	}

	public override void Enter()
	{
		AICharacterMotor.MoveSpeed = Data.RunSpeed;
		AICharacterMotor.TargetPosition = (Transform.position - Vector3.zero) * 50;

		base.Enter();
	}

	public override void Update()
	{
		if (Data.CrowView.transform.position.y < Data.MaxFlightHeight)
		{
			Data.CrowView.transform.Translate(Vector3.up * 3f * Time.deltaTime);

			if (Data.CrowView.transform.position.y > Data.MaxFlightHeight)
			{
				Data.CrowView.transform.Translate(Vector3.down * (Data.CrowView.transform.position.y - Data.MaxFlightHeight));
			}
		}

		base.Update();
	}

	public override void Exit()
	{
		base.Exit();
	}
}