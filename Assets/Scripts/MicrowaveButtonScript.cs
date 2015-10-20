using UnityEngine;
using System.Collections;

public class MicrowaveButtonScript : MonoBehaviour {

    public void OnTriggerEnter(Collider collider)
    {
        GetComponentInParent<MicrowaveScript>().On();
    }
}
