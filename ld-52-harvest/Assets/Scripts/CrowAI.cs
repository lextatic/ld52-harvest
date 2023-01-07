using System;
using UnityEngine;
using static CrowMachineState;

public class CrowAI : MonoBehaviour
{
	public CrowAIData Data;

	private CrowMachineState _currentMovementMachineState;

	public event Action OnHurt;

	[SerializeField]
	private CrowState CurrentState;

	private void Start()
	{
		_currentMovementMachineState = new CrowMove(this);
	}

	private void Update()
	{
		// Atualizar states
		_currentMovementMachineState = _currentMovementMachineState.Handle();
		CurrentState = _currentMovementMachineState.Name;
	}

	private void OnTriggerStay(Collider other)
	{
		if (Data.CrowView.position.y > 3)
		{
			return;
		}

		OnHurt?.Invoke();
	}
}
