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
	public GameObject OnLight;
	public GameObject OffLight;
	public Material GreenDisabled;
	public Material RedEnabled;

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
		if (!LaserDisabled) {
			lasers.SetActive(false);
			LaserDisabled = true;
			if (ActivateSound != null)
				source.PlayOneShot(ActivateSound);
			animation = true;
			OnLight.GetComponent<MeshRenderer>().material = GreenDisabled;
			OffLight.GetComponent<MeshRenderer>().material = RedEnabled;
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
