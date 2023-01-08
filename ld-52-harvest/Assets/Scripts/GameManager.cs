using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Crop;

public class GameManager : MonoBehaviour
{
	[Serializable]
	public struct CropObjective
	{
		public Crop.CropType CropType;
		[Range(0f, 1f)]
		public float MinPercentage;
	}

	public float Duration = 30f;
	public Light Light;
	public Gradient LightGradient;
	public RectTransform TimeMarker;

	public CropUI[] CropUIs;
	public List<CropObjective> CropObjectives;

	public GameObject GameOverPanel;
	public GameObject VictoryPanel;

	private Dictionary<CropType, int> _initialCropCount;
	private Dictionary<CropType, int> _currentCropCount;

	private float _elapsedTime;
	private bool _defeated;
	private bool _victory;

	private IEnumerator Start()
	{
		_defeated = false;
		_victory = false;

		InitializeCropsAndObjectives();

		yield return new WaitForSeconds(Duration);

		SendAllAnimalsHome();

		if (!_defeated)
		{
			GameObject.FindWithTag("Mast").GetComponent<Mast>().ActivateMast();
		}
	}

	public void GoToNextLevel()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			string nextSceneName = SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;
			SceneManager.LoadScene(nextSceneName);
		}
		else
		{
			_victory = true;
			VictoryPanel.SetActive(true);
		}
	}

	public void RestartLevel()
	{
		if (_victory)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

	private void InitializeCropsAndObjectives()
	{
		var allCrops = FindObjectsOfType<Crop>();
		_initialCropCount = new Dictionary<CropType, int>();
		_currentCropCount = new Dictionary<CropType, int>();

		_initialCropCount.Add(CropType.Wheat, 0);
		_initialCropCount.Add(CropType.Corn, 0);
		_initialCropCount.Add(CropType.Carrot, 0);
		_initialCropCount.Add(CropType.Beets, 0);

		_currentCropCount.Add(CropType.Wheat, 0);
		_currentCropCount.Add(CropType.Corn, 0);
		_currentCropCount.Add(CropType.Carrot, 0);
		_currentCropCount.Add(CropType.Beets, 0);

		foreach (var crop in allCrops)
		{
			_initialCropCount[crop.Type]++;
			_currentCropCount[crop.Type]++;

			crop.OnCropDestroyed += Crop_OnCropDestroyed;
		}

		foreach (var cropUI in CropUIs)
		{
			foreach (var cropObjective in CropObjectives)
			{
				if (cropObjective.CropType == cropUI.CropType)
				{
					cropUI.SetMarkerPosition(cropObjective.MinPercentage);
					break;
				}
			}
		}
	}

	private void SendAllAnimalsHome()
	{
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
	}

	private void Crop_OnCropDestroyed(Crop crop)
	{
		_currentCropCount[crop.Type]--;

		var currentPercentageOfCrops = _currentCropCount[crop.Type] / (float)_initialCropCount[crop.Type];

		foreach (var cropUI in CropUIs)
		{
			if (cropUI.CropType == crop.Type)
			{
				cropUI.SetFill(currentPercentageOfCrops);
				break;
			}
		}

		if (!_defeated)
		{
			foreach (var cropObjective in CropObjectives)
			{
				if (cropObjective.CropType == crop.Type)
				{
					if (currentPercentageOfCrops < cropObjective.MinPercentage)
					{
						if (GameOverPanel != null)
						{
							GameOverPanel.SetActive(true);
							_defeated = true;
						}
					}

					break;
				}
			}
		}

		crop.OnCropDestroyed -= Crop_OnCropDestroyed;
	}
}
