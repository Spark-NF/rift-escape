using UnityEngine;
using System.Collections;

public class MicrowaveScript : MonoBehaviour {

    public Light microwaveLight;
    bool on;
    public IceKeyScript iceToMelt;
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
            StopAllCoroutines();
            StartCoroutine(MeltIce());
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!iceToMelt.IsEnabled() && collider == iceToMelt.GetComponent<Collider>())
        {
            Debug.Log(collider.name + " enter");
            iceIsInside = true;
            iceToMelt.GetComponent<FollowingObject>().stopLevitating();
            iceToMelt.transform.position = transform.position;
            iceToMelt.transform.localEulerAngles = new Vector3(0, 14, 0);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (!iceToMelt.IsEnabled() && collider == iceToMelt.GetComponent<Collider>())
        {
            Debug.Log(collider.name + " exit");
            iceIsInside = false;
        }
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
        iceToMelt.GetComponent<IceKeyScript>().EnableKey();
        door.GetComponent<DoorOpen>().Enable();
        if (iceIsInside)
            iceIsInside = false;
    }

}
