using UnityEngine;

public abstract class CrowMachineState
{
	public enum CrowState
	{
		Move, GoToCrop, Eat, Flee, RunAway
	}

	protected enum Event
	{
		Enter, Update, Exit
	}

	[System.Serializable]
	public struct CrowAIData
	{
		[Header("Detection")]
		public Vector2 MinMaxVisibleDistance;
		public float VisibleAngle;

		[Header("Movement Speeds")]
		public float WalkSpeed;
		public float JogSpeed;
		public float RunSpeed;

		[Header("Timed stuff")]
		public float FleeDuration;
		public Vector2 MinMaxIdleDuration;
		public float EatDuration;

		[Header("Position")]
		public float MaxFlightHeight;

		[Header("References")]
		public AISteeringCharacterMotor AICharacterMotor;
		public Transform PlayerTransform;
		public Transform CrowView;

		[HideInInspector]
		public Crop TargetCrop;
	}

	public CrowState Name;

	protected Event Stage;
	protected CrowAI EnemyAI;
	protected CrowMachineState NextState;

	protected CrowAIData Data;
	protected AISteeringCharacterMotor AICharacterMotor;
	protected Transform PlayerTransform;
	protected Crop TargetCrop;
	protected Transform Transform;

	private readonly Collider[] _cropsDetected;

	public CrowMachineState(CrowAI enemyAI)
	{
		EnemyAI = enemyAI;

		Data = enemyAI.Data;
		AICharacterMotor = Data.AICharacterMotor;
		PlayerTransform = Data.PlayerTransform;
		TargetCrop = Data.TargetCrop;
		Transform = AICharacterMotor.transform;

		_cropsDetected = new Collider[10];
	}

	public virtual void Enter() { Stage = Event.Update; }
	public virtual void Update() { Stage = Event.Update; }
	public virtual void Exit() { Stage = Event.Enter; }

	public CrowMachineState Handle()
	{
		switch (Stage)
		{
			case Event.Enter:
				Enter();
				break;

			case Event.Update:
				Update();
				break;

			case Event.Exit:
				Exit();
				return NextState;
		}

		return this;
	}

	protected bool CanSeeCrop()
	{
		var hits = Physics.OverlapSphereNonAlloc(AICharacterMotor.transform.position,
			Data.MinMaxVisibleDistance.y,
			_cropsDetected,
			1 << LayerMask.NameToLayer("Crop"));

		for (int i = 0; i < hits; i++)
		{
			var crop = _cropsDetected[i].GetComponent<Crop>();

			if (!crop.IsOcupied)
			{
				Vector3 direction = _cropsDetected[i].transform.position - EnemyAI.Data.AICharacterMotor.transform.position;

				float angle = Vector3.Angle(direction, EnemyAI.Data.AICharacterMotor.transform.forward);
				if (direction.magnitude >= Data.MinMaxVisibleDistance.x && angle <= Data.VisibleAngle)
				{
					EnemyAI.Data.TargetCrop = crop;
					return true;
				}
			}
		}

		return false;
	}
}
