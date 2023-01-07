public abstract class CrowDetecting : CrowMachineState
{
	public CrowDetecting(CrowAI enemyAI)
		: base(enemyAI)
	{
		//
	}

	public override void Enter()
	{
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
		if (Data.GoHome)
		{
			NextState = new CrowGoHome(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		if (CanSeeCrop())
		{
			NextState = new CrowGoToCrop(EnemyAI);
			Stage = Event.Exit;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
