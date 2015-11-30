using UnityEngine;
using System.Collections;

public class MainDoor : DoorOpen {
    public AudioClip lockedsound;
    public AudioClip unlocksound;
    public static bool Locked = true;
    bool wasLocked = true;

    public GameObject Key;

    void OnTriggerEnter(Collider other)
    {
        if (Locked)
        {
            if (other == Key.GetComponent<Collider>())
            {
                Debug.Log("main door unlocked");
                Locked = false;
                Destroy(Key);
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
