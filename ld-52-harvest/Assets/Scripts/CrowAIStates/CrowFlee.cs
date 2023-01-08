using UnityEngine;

public class CrowFlee : CrowMachineState
{
	private float _hurtTimer;
	private float _heightIncreaseVelocity;
	private Vector3 _hurtDirection;

	public CrowFlee(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.Flee;
	}

	public override void Enter()
	{
		_hurtTimer = Data.FleeDuration;
		AICharacterMotor.MoveSpeed = Data.RunSpeed;
		_hurtDirection = Transform.position - PlayerTransform.position;
		_hurtDirection.y = 0;
		_hurtDirection.Normalize();

		Transform.LookAt(Transform.position + _hurtDirection);

		_heightIncreaseVelocity = (Data.MaxFlightHeight - Transform.position.y) / _hurtTimer;

		Data.WingsAudioEvent.Play(Data.AudioSource);

		base.Enter();
	}

	public override void Update()
	{
		_hurtTimer -= Time.deltaTime;

		AICharacterMotor.TargetPosition = Transform.position + _hurtDirection;

		Data.CrowView.transform.Translate(Vector3.up * _heightIncreaseVelocity * Time.deltaTime);

		if (_hurtTimer <= 0)
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
