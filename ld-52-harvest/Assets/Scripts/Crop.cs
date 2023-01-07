using UnityEngine;

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

	[HideInInspector]
	public bool IsOcupied = false;
}
