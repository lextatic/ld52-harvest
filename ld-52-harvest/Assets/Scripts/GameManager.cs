using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float Duration = 30f;
	public Light Light;
	public Gradient LightGradient;
	public RectTransform TimeMarker;

	private float _elapsedTime;

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

		GameObject.FindWithTag("Mast").GetComponent<SphereCollider>().enabled = true;

		if (FindObjectsOfType<Crop>().GroupBy(x => x.Type).Select(x => x.First()).Count() < 4)
		{
			Debug.Log("Defeat");
		}
		else
		{
			Debug.Log("Victory");
		}
	}

	private void Update()
	{
		_elapsedTime += Time.deltaTime;
		float t = _elapsedTime / Duration;

		if (t > 1)
		{
			t = 1;
		}

		Light.color = LightGradient.Evaluate(t);

		// -145 ~ 145
		TimeMarker.anchoredPosition = new Vector2((t * 290) - 145, TimeMarker.anchoredPosition.y);
	}
}
