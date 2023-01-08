using System;
using UnityEngine;
using static RatMachineState;

public class RatAI : MonoBehaviour
{
	public RatAIData Data;

	private RatMachineState _currentMovementMachineState;

	public event Action OnHurt;

	[SerializeField]
	private RatState _currentState;

	public void GoHome()
	{
		Data.GoHome = true;
	}

	private void Start()
	{
		Data.PlayerTransform = GameObject.FindWithTag("Player").transform;
		_currentMovementMachineState = new RatGoToFarm(this);

		Data.AudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		_currentMovementMachineState = _currentMovementMachineState.Handle();
		_currentState = _currentMovementMachineState.Name;
	}

	private void OnTriggerStay(Collider other)
	{
		OnHurt?.Invoke();
	}
}
