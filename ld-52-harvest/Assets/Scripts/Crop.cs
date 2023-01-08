using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
	public enum CropType
	{
		Wheat,
		Corn,
		Carrot,
		Beets
	}

	public CropType Type;

	public Image Image;

	public SimpleAudioEvent CropAudioEvent;

	private AudioSource _audioSource;
	private bool _isOcupied = false;

	public float FillAmmount
	{
		set => Image.fillAmount = value;
	}

	public bool IsOcupied
	{
		get => _isOcupied;

		set
		{
			if (value && !_isOcupied)
			{
				CropAudioEvent.Play(_audioSource);
			}

			_isOcupied = value;
		}
	}

	private void Start()
	{
		Image.transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));

		var uiCanvas = GameObject.FindWithTag("UICanvas").transform;
		Image.transform.SetParent(uiCanvas, true);

		_audioSource = GetComponent<AudioSource>();
	}

	private void OnDestroy()
	{
		if (Image != null)
		{
			Destroy(Image.gameObject);
		}
	}
}
