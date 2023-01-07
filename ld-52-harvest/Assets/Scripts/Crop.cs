using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
	public enum CropType
	{
		Wheat,
		Corn,
		Carrot,
		Beats
	}

	public CropType Type;

	public Image Image;

	public float FillAmmount
	{
		set => Image.fillAmount = value;
	}

	[HideInInspector]
	public bool IsOcupied = false;

	private void Start()
	{
		Image.transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));

		var uiCanvas = GameObject.FindWithTag("UICanvas").transform;
		Image.transform.SetParent(uiCanvas, true);
	}

	private void OnDestroy()
	{
		if (Image != null)
		{
			Destroy(Image.gameObject);
		}
	}
}
