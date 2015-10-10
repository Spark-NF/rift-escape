using UnityEngine;
using System.Collections;

public class FollowingObject : MonoBehaviour {

	public Vector3 LevitationPosition;
	public GameObject LevitationAnchor;
	public float animSpeed = 10.0f;

	private bool _isLevitating = false;
	private Rigidbody _rigidbody;
	private float fireRate = 1.0f;
	private float nextFire = 0.0f;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (Time.time > nextFire && other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ()) {
			_isLevitating = !_isLevitating;
			_rigidbody.useGravity = !_isLevitating;
			nextFire = Time.time + fireRate;
		}
	}

	// Update is called once per frame
	void Update () {
		if (_isLevitating) {
			Vector3 offPosition = LevitationAnchor.transform.position + LevitationAnchor.transform.rotation * LevitationPosition;
			_rigidbody.AddForce((offPosition - transform.position) * animSpeed);
			_rigidbody.velocity = (offPosition - transform.position) * animSpeed;
			/*transform.position = new Vector3(GetChange(offPosition.x, transform.position.x),
			                                      GetChange(offPosition.y, transform.position.y),
			                                      GetChange(offPosition.z, transform.position.z));*/
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
