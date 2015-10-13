using UnityEngine;
using System.Collections;

public class MicrowaveScript : MonoBehaviour {

    public Light microwaveLight;
    bool on;
    public GameObject iceToMelt;
    public GameObject door;

    bool activated;

    bool iceIsInside;

    public bool IsDoorClosed()
    {
        Vector3 closedRotation = door.GetComponent<DoorOpen>().closedrotation;
        return (int)door.transform.localRotation.eulerAngles.x == (int)closedRotation.x &&
                (int)door.transform.localRotation.eulerAngles.y == (int)closedRotation.y &&
                (int)door.transform.localRotation.eulerAngles.z == (int)closedRotation.z;
    }

    public void On()
    {
        if (IsDoorClosed())
        {
            if (activated)
                return;
            activated = true;
            door.GetComponent<DoorOpen>().Disable();
            microwaveLight.gameObject.SetActive(true);
            if (iceIsInside)
                StartCoroutine(MeltIce());
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider = iceToMelt.GetComponent<Collider>())
            iceIsInside = true;
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider = iceToMelt.GetComponent<Collider>())
            iceIsInside = false;
    }

    IEnumerator MeltIce()
    {
        for (int i = 0; i < 500; i++)
        {
            if (iceIsInside)
                iceToMelt.GetComponent<IceKeyScript>().ScaleIce();
            yield return null;
        }

        activated = false;
        microwaveLight.gameObject.SetActive(false);
        iceToMelt.GetComponent<IceKeyScript>().SetKeyUsabled();
        door.GetComponent<DoorOpen>().Disable();
    }

}
