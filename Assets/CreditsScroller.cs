using UnityEngine;
using System.Collections;

public class CreditsScroller : MonoBehaviour
{
	public GameObject content;
	public float initScroll;

	void Start ()
	{
		content.transform.Translate(0, -initScroll, 0);
	}

	void Update()
	{
		content.transform.Translate(0, Time.deltaTime / 1.0f, 0);

		if (content.transform.localPosition.y > 3)
			content.transform.Translate(0, -initScroll - 4, 0);
	}
}
