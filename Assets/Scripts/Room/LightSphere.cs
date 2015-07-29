using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LightSphere : MonoBehaviour {
	public Light light;
	public List<Color> colors;
	public GameObject o;

	private Color c;

	// Use this for initialization
	void Start () {
		if (colors.Any())
			c = colors.First();
	}

	int current_index = 0;
	float change_speed = .005f;
	float dr, dg, db;
	float approx = .01f; // used to know when two colors are close.
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.SqrMagnitude(new Vector3(c.r,c.g,c.b) - new Vector3(colors.ElementAt(current_index).r, colors.ElementAt(current_index).g, colors.ElementAt(current_index).b)) < approx)
		{
		    current_index++;
			if (current_index == colors.Count)
				current_index = 0;
			var next = colors.ElementAt(current_index);
			dr = (next.r - c.r) * change_speed;
			dg = (next.g - c.g) * change_speed;
			db = (next.b - c.b) * change_speed;
		}
		c.r += dr;
		c.g += dg;
		c.b += db;
		light.color = c;
		GetComponent<Renderer>().material.color = c;
	}
}
