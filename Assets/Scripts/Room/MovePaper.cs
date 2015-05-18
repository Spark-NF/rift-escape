using UnityEngine;
using System.Collections;

public class MovePaper : MonoBehaviour {
	public GameObject paper;
	private int direction;

	void OnTriggerEnter(Collider other)
	{
		enabled = true;
		(paper.GetComponent(typeof(BoxCollider)) as BoxCollider).isTrigger = false;
	}

	// Use this for initialization
	void Start () {
		enabled = false;
		direction = -1;
	}
	
	// Update is called once per frame
	void Update () {

		paper.transform.Rotate(direction * 5, 0, 0);
		paper.transform.Translate(0, -direction * 0.025f, direction * 0.025f);
		if ((paper.transform.localEulerAngles.x < 5 && direction == -1)
		    || (paper.transform.localEulerAngles.x >= 90 && direction == 1))
		{
			enabled = false;
			direction = -direction;
			(paper.GetComponent(typeof(BoxCollider)) as BoxCollider).isTrigger = true;
		}
	}
}
