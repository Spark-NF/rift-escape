using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {
	public AudioSource source = null;
	public AudioClip opensound;
	public AudioClip closesound;
	public Vector3 closedrotation;
	public Vector3 openedrotation;
	public float rotationspeed;
	private bool opened = false;
	private bool animating = false;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) 
	{
		OpenAction (other);
	}

	protected void OpenAction(Collider other) 
	{
		if (!animating && other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
		{
			opened = !opened;
			animating = true;
			if(source != null && opened && opensound != null)
				source.PlayOneShot(opensound);
			if(source != null && !opened && closesound != null)
				source.PlayOneShot(closesound);
		}
	}
	
	// Update is called once per frame
	void Update() {
		UpdateAction();
	}

	protected void UpdateAction() {
		if (animating) {
			Vector3 destination = opened? openedrotation : closedrotation;
			if (Vector3.SqrMagnitude(transform.rotation.eulerAngles - destination) < 1f) { // animation finished
				animating = false;
				return;
			}
			Vector3 euler = new Vector3(GetAnglechange(destination.x, transform.rotation.eulerAngles.x),
			                      GetAnglechange(destination.y, transform.rotation.eulerAngles.y),
			                      GetAnglechange(destination.z, transform.rotation.eulerAngles.z));
			transform.rotation = Quaternion.Euler(euler + transform.rotation.eulerAngles);
		}
	}

	private float GetAnglechange(float destination, float current)
	{
		float diff = destination - current;
		if (Mathf.Abs (diff) <= rotationspeed) {
			return diff;
		}
		return diff < 0 ? -rotationspeed : rotationspeed;
	}

}
