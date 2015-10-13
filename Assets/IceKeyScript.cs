using UnityEngine;
using System.Collections;

public class IceKeyScript : MonoBehaviour {

    public GameObject Ice;
    public GameObject Key;

    bool keyIsUsabled = false;

    public void ScaleIce()
    {
        Ice.transform.localScale *= 0.995f;
    }

    public void SetKeyUsabled()
    {
        keyIsUsabled = true;
    }
}
