using System;
using UnityEngine;
using static RatMachineState;

public class RatAI : MonoBehaviour
{
	public RatAIData Data;

	//private AICharacterMotor _characterMotor;
	private AttackAnimator _attackAnimator;

	private RatMachineState _currentMovementMachineState;

	public event Action OnHurt;

	[SerializeField]
	private RatState CurrentState;

	private void Start()
	{
		_currentMovementMachineState = new RatMove(this);
	}

	private void Update()
	{
		// Atualizar states
		_currentMovementMachineState = _currentMovementMachineState.Handle();
		CurrentState = _currentMovementMachineState.Name;
	}

	private void OnTriggerStay(Collider other)
	{
		OnHurt?.Invoke();
	}
}
