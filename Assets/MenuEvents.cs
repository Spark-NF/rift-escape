using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour
{
	public void MenuQuit()
	{
		Application.Quit();
	}

	public void MenuPlay()
	{
		Application.LoadLevel("Room");
	}

	public void MenuOptions()
	{
		Debug.LogWarning("Options");
	}
}
