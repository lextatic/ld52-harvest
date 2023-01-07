using UnityEngine;

public class RatIdle : Detecting
{
	private float _idleTimer;

	public RatIdle(RatAI enemyAI)
		: base(enemyAI)
	{
		Name = RatState.Idle;
	}

	public override void Enter()
	{
		_idleTimer = Random.Range(Data.MinMaxIdleDuration.x, Data.MinMaxIdleDuration.y);
		base.Enter();
	}

	public override void Update()
	{
		_idleTimer -= Time.deltaTime;

		if (_idleTimer <= 0)
		{
			NextState = new RatMove(EnemyAI);

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
