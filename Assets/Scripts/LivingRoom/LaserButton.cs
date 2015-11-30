using UnityEngine;
using System.Collections;

public class LaserButton : MonoBehaviour {

	public GameObject lasers;
	public Vector3 offPosition;
	public float animSpeed;
	public AudioClip ActivateSound;
	private bool animation = false;
	public static bool LaserDisabled = false;
	public AudioSource source;
    public LED LEDs;

	// Update is called once per frame
	void Update () {
		if (animation) {
			transform.localPosition = new Vector3(GetChange(offPosition.x, transform.localPosition.x),
												  GetChange(offPosition.y, transform.localPosition.y),
												  GetChange(offPosition.z, transform.localPosition.z));
			animation = !(Vector3.SqrMagnitude(transform.localPosition - offPosition) < 0.0001f);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>()
            && !LaserDisabled) {
			lasers.SetActive(false);
			LaserDisabled = true;
			if (ActivateSound != null)
				source.PlayOneShot(ActivateSound);
			animation = true;
            LEDs.SetOff();
		}
	}

	private float GetChange(float destination, float current)
	{
		float diff = destination - current;
		if (Mathf.Abs(diff) <= animSpeed) {
			return diff;
		}
		return diff < 0 ? current - animSpeed : current + animSpeed;
	}

}
