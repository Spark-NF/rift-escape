using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class CircuitBreaker : MonoBehaviour {

    public List<GameObject> lights;
    public GameObject lasers;

    public bool activated;

    float rotation = 30f;
    float speed = 2;

    bool canBeTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (canBeTriggered)
        {
            canBeTriggered = false;
            ToggleCircuit();
        }
    }

    void OnTriggerExit()
    {
        canBeTriggered = true;
    }

    public void ToggleCircuit()
    {
        StopAllCoroutines();
        if (activated)
            StartCoroutine("SetCircuitOff");
        else
            StartCoroutine("SetCircuitOn");
    }

    IEnumerator SetCircuitOff()
    {
        GetComponent<BoxCollider>().enabled = false;
        float count = rotation / speed;
        while (count > 0)
        {
            transform.Rotate(-speed, 0f, 0f);
            count--;
            yield return null;
        }
        activated = false;
        foreach (GameObject l in lights)
            l.SetActive(false);


        lasers.SetActive(false);
        LaserButton.LaserDisabled = true;

        GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator SetCircuitOn()
    {
        GetComponent<BoxCollider>().enabled = false;
        float count = rotation / speed;
        while (count > 0)
        {
            transform.Rotate(speed, 0f, 0f);
            count--;
            yield return null;
        }
        activated = true;
        foreach (GameObject l in lights)
            l.SetActive(true);
        if (!LaserButton.LaserDisabled)
            lasers.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
    }
}
