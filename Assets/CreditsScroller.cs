using UnityEngine;
using System.Collections;

public class CreditsScroller : MonoBehaviour
{
	public GameObject content;
    public float speed = 1.0f;
	public float initScroll;

	void Start ()
	{
		content.transform.Translate(0, -initScroll, 0);
	}

	void Update()
	{
		content.transform.Translate(0, speed * Time.deltaTime, 0);

		if (content.transform.localPosition.y > 3)
			content.transform.Translate(0, -initScroll - 4, 0);
	}
}
