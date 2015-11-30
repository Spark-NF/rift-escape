using UnityEngine;
using System.Collections;

public class SafeDoor : DoorOpen
{
    private float rotationNb = 0;
    public GameObject handle;
    void Update()
    {
        if (animating)
        {
            if (opened)
            {
                if (rotationNb < 180)
                {
                    handle.transform.Rotate(Vector3.forward, rotationspeed);
                    rotationNb += rotationspeed;
                }
                else
                {
                    UpdateAction();
                    if (!animating) // animation finished
                        rotationNb = 0;
                }
            }
            else
            {
                    UpdateAction();
                    if (!animating) // animation finished
                        rotationNb = 180;
            }
        }
        else if (rotationNb > 0)
        {
            handle.transform.Rotate(Vector3.forward, rotationspeed);
            rotationNb -= rotationspeed;
        }
    }
}
