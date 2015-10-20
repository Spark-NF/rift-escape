using UnityEngine;
using System.Collections;

public class IceKeyScript : MonoBehaviour {

    public GameObject Ice;
    public GameObject Key;

    bool keyEnabled = false;

    public void ScaleIce()
    {
        Ice.transform.localScale *= 0.995f;
    }

    public void EnableKey()
    {
        keyEnabled = true;
    }

    public bool IsEnabled()
    {
        return keyEnabled;
    }
}
