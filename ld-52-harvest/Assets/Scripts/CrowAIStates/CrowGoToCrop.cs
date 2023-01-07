using UnityEngine;

public class CrowGoToCrop : CrowMachineState
{
	private float _heightDecreaseVelocity;

	public CrowGoToCrop(CrowAI enemyAI)
		: base(enemyAI)
	{
		Name = CrowState.Move;
	}

	public override void Enter()
	{
		if (TargetCrop == null || TargetCrop.IsOcupied)
		{
			NextState = new CrowMove(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		AICharacterMotor.MoveSpeed = Data.JogSpeed;
		AICharacterMotor.TargetPosition = TargetCrop.transform.position;
		AICharacterMotor.TargetPosition.y = 0;

		var distance = Vector3.Distance(Transform.position, AICharacterMotor.TargetPosition);
		var time = (distance - 0.1f) / AICharacterMotor.MoveSpeed;

		_heightDecreaseVelocity = Data.CrowView.transform.position.y / time;

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
		if (TargetCrop == null || TargetCrop.IsOcupied)
		{
			NextState = new CrowMove(EnemyAI);
			Stage = Event.Exit;
			return;
		}

		if (Data.CrowView.transform.position.y > 0)
		{
			Data.CrowView.transform.Translate(Vector3.down * _heightDecreaseVelocity * Time.deltaTime);

			if (Data.CrowView.transform.position.y < 0)
			{
				Data.CrowView.transform.Translate(Vector3.up * -Data.CrowView.transform.position.y);
			}
		}

		if (Vector3.Distance(Transform.position, AICharacterMotor.TargetPosition) <= 0.2f)
		{
			NextState = new CrowEat(EnemyAI);
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
