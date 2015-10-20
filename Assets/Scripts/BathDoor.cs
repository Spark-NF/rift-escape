using UnityEngine;
using System.Collections;

public class BathDoor : DoorOpen {
    public AudioClip lockedsound;
    public AudioClip unlocksound;
    public static bool Locked = true;
    bool wasLocked = true;

    public IceKeyScript key;

    void OnTriggerEnter(Collider other)
    {
        if (Locked)
        {
            if (other.gameObject == key && key.IsEnabled())
            {
                Locked = false;
                Destroy(key);
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
