using UnityEngine;
using System.Collections;

public class LED : MonoBehaviour {

    public GameObject OnLed;
    public GameObject OffLed;

    public Material GreenEnabled;
    public Material GreenDisabled;
    public Material RedEnabled;
    public Material RedDisabled;

    public void SetOn()
    {
        onMaterial = GreenEnabled;
        offMaterial = RedDisabled;
    }

    public void SetOff()
    {
        onMaterial = GreenDisabled;
        offMaterial = RedEnabled;
    }

    public Material onMaterial
    {
        get { return OnLed.GetComponent<Renderer>().material; }
        set { OnLed.GetComponent<Renderer>().material = value; }
    }
    public Material offMaterial
    {
        get { return OffLed.GetComponent<Renderer>().material; }
        set { OffLed.GetComponent<Renderer>().material = value; }
    }

}
