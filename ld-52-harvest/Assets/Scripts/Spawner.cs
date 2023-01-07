using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject Prefab;
	public int SpawnQuantity;
	public float SpawnerRadius;
	public float SpawnStartDelay;
	public float SpawnFrequency;

	private IEnumerator Start()
	{
		var frequency = new WaitForSeconds(SpawnFrequency);

		yield return new WaitForSeconds(SpawnStartDelay);

		for (int i = 0; i < SpawnQuantity; i++)
		{
			var randomDirection2D = (Random.insideUnitCircle.normalized * SpawnerRadius);
			Instantiate(Prefab, new Vector3(randomDirection2D.x, 0, randomDirection2D.y), Quaternion.identity);

			yield return frequency;
		}
	}
}
