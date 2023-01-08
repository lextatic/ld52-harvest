using TMPro;
using UnityEngine;

public class Mast : MonoBehaviour
{
	public TextMeshProUGUI[] ZZZText;
	public GameObject TextInstructions;
	public Color DisabledColor;
	public Color EnabledColor;

	public void ActivateMast()
	{
		ZZZText[0].gameObject.SetActive(true);
		GetComponent<SphereCollider>().enabled = true;
	}

	private void Start()
	{
		ZZZText[0].gameObject.SetActive(false);
		TextInstructions.SetActive(false);

		foreach (var text in ZZZText)
		{
			text.color = DisabledColor;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		foreach (var text in ZZZText)
		{
			text.color = EnabledColor;
		}

		TextInstructions.SetActive(true);
	}

	private void OnTriggerExit(Collider other)
	{
		foreach (var text in ZZZText)
		{
			text.color = DisabledColor;
		}

		TextInstructions.SetActive(false);
	}
}
