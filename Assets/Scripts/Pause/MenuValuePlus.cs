using UnityEngine;
using System.Collections;

public class MenuValuePlus : MenuItem
{
	public GameObject menuValue;
	private MenuValue menu;
	
	public void Start()
	{
		menu = menuValue.GetComponent<MenuValue>();
	}
	
	public override void activate()
	{
		menu.plus();
	}
}
