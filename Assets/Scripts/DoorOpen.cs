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
    private bool disabled = false;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other) 
	{
        if (!disabled)
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
			Vector3 local = transform.localRotation.eulerAngles;
			if (Vector3.SqrMagnitude(local - destination) < 1f) { // animation finished
				animating = false;
				return;
			}
			Vector3 euler = new Vector3(GetAnglechange(destination.x, local.x),
			                            GetAnglechange(destination.y, local.y),
			                            GetAnglechange(destination.z, local.z));
			transform.localRotation = Quaternion.Euler(euler + local);
		}
	}

	private float GetAnglechange(float destination, float current)
	{
		// if the rotaton goes bellow 0, i have to cheat by moving the angle so that i take the shortest way
		if (Mathf.Abs(destination - current) > 180)
		    current += destination > current ? 360 : -360;
		float diff = destination - current;
		if (Mathf.Abs (diff) <= rotationspeed) {
			return diff;
		}
		return diff < 0 ? -rotationspeed : rotationspeed;
	}

    public void Disable()
    {
        disabled = true;
    }

    public void Enable()
    {
        disabled = false;
    }

}
