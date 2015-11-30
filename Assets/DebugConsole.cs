using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebugConsole : MonoBehaviour
{
	public GameObject canvas = null;
	public GameObject input = null;
	public KeyCode toggleKey = KeyCode.O;

	protected bool visible = false;
	protected InputField inputField = null;

	void Start()
	{
		inputField = input.GetComponent<InputField>();
	}

	void Update()
	{
		if (Input.GetKeyDown(toggleKey) && !visible)
		{
			toggleConsole();
		}
	}

	public void toggleConsole()
	{
		visible = !visible;
		canvas.SetActive(visible);
        OVRPlayerController.paused = visible;
	    PauseMenu.CanUsePKey = !visible;

		if (visible)
		{
			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
			inputField.OnPointerClick(new PointerEventData(EventSystem.current));
		}
	}

	public void DebugCommand(string msg)
	{
		string command = inputField.text;

		switch (command)
		{
			case "quit":
			case "exit":
				Application.Quit();
				break;
            case "open room":
		        LockedRoomDoor.Locked = false;
		        break;
			default:
                AdvancedCommand(command);
				Debug.Log("Debug command: '" + command + "'");
				break;
		}
		
		inputField.text = "";
		toggleConsole();
	}

    private void AdvancedCommand(string command)
    {
        if (command.StartsWith("disable"))
        {
            var args = command.Split(' ');
            if (args.Length < 2)
                return;
            GameObject obj = GameObject.Find(args[1]);
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
