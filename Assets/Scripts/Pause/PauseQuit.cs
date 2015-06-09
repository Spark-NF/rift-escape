using UnityEngine;
using System.Collections;

public class PauseQuit : MenuItem
{
	public override void activate()
	{
		Application.Quit();
	}
}
