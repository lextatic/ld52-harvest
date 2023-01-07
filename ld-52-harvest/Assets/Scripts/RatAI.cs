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
	public event Action OnGoHome;

	[SerializeField]
	private RatState CurrentState;

	public void GoHome()
	{
		Data.GoHome = true;
	}

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
