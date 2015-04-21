using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
	public float time = 1f;
	public float startingAlpha = 0f;
	public float endAlpha = 1f;
	private CanvasGroup canvasGroup;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start ()
	{
		canvasGroup.alpha = startingAlpha;
		StartCoroutine("Fade");
	}
	
	IEnumerator Fade()
	{
		while (canvasGroup.alpha < endAlpha)
		{
			canvasGroup.alpha += Time.deltaTime / time;
			if (canvasGroup.alpha > endAlpha)
			{
				canvasGroup.alpha = endAlpha;
			}
			yield return null;
		}
	}
}
