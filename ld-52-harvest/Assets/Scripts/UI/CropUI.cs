using UnityEngine;
using UnityEngine.UI;

public class CropUI : MonoBehaviour
{
	public Crop.CropType CropType;
	public RectTransform Marker;
	public Image FillSprite;

	public void SetMarkerPosition(float percentage)
	{
		// 4 ~ 44
		Marker.anchoredPosition = new Vector2(Marker.anchoredPosition.x, 4 + (percentage * 40));
	}

	public void SetFill(float percentage)
	{
		if (FillSprite == null)
		{
			return;
		}

		percentage = (percentage * 0.8f) + 0.1f;
		FillSprite.fillAmount = percentage;
	}

	private void Start()
	{
		SetFill(1);
	}
}
