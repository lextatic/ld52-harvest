public abstract class RatDetecting : RatMachineState
{
	public RatDetecting(RatAI enemyAI)
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
		NextState = new RatFlee(EnemyAI);
		Stage = Event.Exit;
		EnemyAI.OnHurt -= EnemyAI_OnHurt;
	}

	public override void Update()
	{
		if (Data.GoHome)
		{
			NextState = new RatGoHome(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		if (CanSeeCrop())
		{
			NextState = new RatGoToCrop(EnemyAI);
			Stage = Event.Exit;
		}
	}

	public override void Exit()
	{
		base.Exit();
	}
}
