using UnityEngine;
using System.Collections;

public class BathDoor : DoorOpen {
    public AudioClip lockedsound;
    public AudioClip unlocksound;
    public static bool Locked = true;
    bool wasLocked = true;

    public IceKeyScript key;
    public GameObject KeyModel;

    void OnTriggerEnter(Collider other)
    {
        if (Locked)
        {
            if (other == key.GetComponent<Collider>() && key.IsEnabled())
            {
                Debug.Log("bath door unlocked");
                Locked = false;
                Destroy(KeyModel);
            }
            else if (lockedsound != null && !source.isPlaying)
            {
                source.clip = lockedsound;
                source.Play();
            }
        }
        else
            base.OpenAction(other);
    }

    void Update()
    {
        if (wasLocked ^ Locked)
        { // state changed
            source.clip = unlocksound;
            source.Play();
            wasLocked = Locked;
        }
        if (!Locked)
            base.UpdateAction();
    }
}
