using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float Duration = 30f;

	// Start is called before the first frame update
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(Duration);

		var allRats = FindObjectsOfType<RatAI>();
		foreach (var rat in allRats)
		{
			rat.GoHome();
		}

		var allCrows = FindObjectsOfType<CrowAI>();
		foreach (var crow in allCrows)
		{
			crow.GoHome();
		}

		if (FindObjectsOfType<Crop>().GroupBy(x => x.Type).Select(x => x.First()).Count() < 4)
		{
			Debug.Log("Defeat");
		}
		else
		{
			Debug.Log("Victory");
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
