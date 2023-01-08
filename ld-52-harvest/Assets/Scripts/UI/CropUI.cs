using UnityEngine;
using UnityEngine.UI;

public class CropUI : MonoBehaviour
{
	public Crop.CropType CropType;
	public RectTransform Marker;
	public Image FillSprite;

	private int _initialCropCount = 0;
	private int _currentCropCount = 0;

	public void SetMarkerPosition(float percentage)
	{
		// 4 ~ 44
		Marker.anchoredPosition = new Vector2(Marker.anchoredPosition.x, 4 + (percentage * 40));
	}

	private void SetFill(float percentage)
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
		SetMarkerPosition(0.8f);
		var allCrops = FindObjectsOfType<Crop>();

		foreach (Crop crop in allCrops)
		{
			if (crop.Type == CropType)
			{
				_initialCropCount++;
				_currentCropCount++;

				crop.OnCropDestroyed += Crop_OnCropDestroyed;
			}
		}

		SetFill(1);
	}

	private void Crop_OnCropDestroyed(Crop crop)
	{
		_currentCropCount--;

		SetFill(_currentCropCount / (float)_initialCropCount);

		crop.OnCropDestroyed -= Crop_OnCropDestroyed;
	}
}
