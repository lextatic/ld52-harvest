using System.Collections;
using UnityEngine;

namespace Modulo18
{
	public class AttackAnimator : MonoBehaviour
	{
		public MeshRenderer MeshRenderer;
		public GameObject AttackCollider;

		public void PlayAttackAnim()
		{
			StartCoroutine(AttackAnimCoroutine());
		}

		private IEnumerator AttackAnimCoroutine()
		{
			AttackCollider.SetActive(true);
			MeshRenderer.material.color = Color.red;
			yield return new WaitForSeconds(0.5f);
			AttackCollider.SetActive(false);
			MeshRenderer.material.color = Color.white;
		}
	}
}
