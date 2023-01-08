using System;
using UnityEngine;
using static CrowMachineState;

public class CrowAI : MonoBehaviour
{
	public CrowAIData Data;

	private CrowMachineState _currentMovementMachineState;

	public event Action OnHurt;

	[SerializeField]
	private CrowState _currentState;

	public void GoHome()
	{
		Data.GoHome = true;
	}
	private void Start()
	{
		Data.PlayerTransform = GameObject.FindWithTag("Player").transform;
		_currentMovementMachineState = new CrowGoToFarm(this);

		Data.AudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		_currentMovementMachineState = _currentMovementMachineState.Handle();
		_currentState = _currentMovementMachineState.Name;
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
