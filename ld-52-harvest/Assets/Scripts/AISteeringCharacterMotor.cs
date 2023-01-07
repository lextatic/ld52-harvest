using UnityEngine;

public class AISteeringCharacterMotor : MonoBehaviour
{
	public float MoveSpeed;

	public Vector3 TargetPosition;

	private CharacterController characterController;



	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		var targetDirection = TargetPosition - transform.position;
		targetDirection.y = 0;

		Vector3 rotatedDirection = Vector3.RotateTowards(transform.forward, targetDirection, Mathf.PI * Time.deltaTime, 1);

		characterController.SimpleMove(rotatedDirection.normalized * MoveSpeed);

		transform.LookAt(transform.position + rotatedDirection.normalized);
	}
}
