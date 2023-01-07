using UnityEngine;

namespace Modulo18
{
	public class AICharacterMotor : MonoBehaviour
	{
		public float MoveSpeed;

		public Vector3 TargetPosition;

		private CharacterController characterController;

		public bool ReverseDirection = false;

		private void Awake()
		{
			characterController = GetComponent<CharacterController>();
		}

		private void Update()
		{
			var direction = TargetPosition - transform.position;
			direction.y = 0;

			if (direction.magnitude < 0.1f)
				return;

			characterController.SimpleMove(direction.normalized * MoveSpeed);

			if (ReverseDirection)
			{
				transform.LookAt(transform.position - direction.normalized);
			}
			else
			{
				transform.LookAt(transform.position + direction.normalized);
			}
		}
	}
}
