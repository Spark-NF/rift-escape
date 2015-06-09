using UnityEngine;
using System.Collections;

public class PauseMainMenu : MenuItem
{
	public override void activate()
	{
		Application.LoadLevel("MainMenu");
	}
}

