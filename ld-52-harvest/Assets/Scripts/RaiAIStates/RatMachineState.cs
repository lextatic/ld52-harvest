using UnityEngine;

public abstract class RatMachineState
{
	public enum RatState
	{
		Move, Idle, GoToCrop, Eat, Flee, RunAway
	}

	protected enum Event
	{
		Enter, Update, Exit
	}

	[System.Serializable]
	public struct RatAIData
	{
		[Header("Detection")]
		public float VisibleDistance;
		//public float VisibaleAngle;
		//public float AttackDistance;

		[Header("Movement Speeds")]
		public float WalkSpeed;
		public float JogSpeed;
		public float RunSpeed;

		[Header("Timed stuff")]
		public float FleeDuration;
		public Vector2 MinMaxIdleDuration;
		public float EatDuration;

		[Header("References")]
		public AICharacterMotor AICharacterMotor;
		public Transform PlayerTransform;

		[HideInInspector]
		public Crop TargetCrop;
	}

	public RatState Name;

	protected Event Stage;
	protected RatAI EnemyAI;
	protected RatMachineState NextState;

	protected RatAIData Data;
	protected AICharacterMotor AICharacterMotor;
	protected Transform PlayerTransform;
	protected Crop TargetCrop;
	protected Transform Transform;

	private readonly Collider[] _cropsDetected;

	public RatMachineState(RatAI enemyAI)
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

	public RatMachineState Handle()
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
			Data.VisibleDistance,
			_cropsDetected,
			1 << LayerMask.NameToLayer("Crop"));

		for (int i = 0; i < hits; i++)
		{
			var crop = _cropsDetected[Random.Range(0, hits)].GetComponent<Crop>();

			if (!crop.IsOcupied)
			{
				EnemyAI.Data.TargetCrop = crop;
				return true;
			}
		}

		return false;
	}

	//protected bool CanAttackPlayer()
	//{
	//	Vector3 direction = Player.position - AICharacterMotor.transform.position;
	//	if (direction.magnitude <= AttackDistance)
	//	{
	//		return true;
	//	}

	//	return false;
	//}
}
