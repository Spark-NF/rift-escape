using UnityEngine;
using System.Collections;

public class PlayPiano : MonoBehaviour {
    public AudioSource source = null;
    public AudioClip sound;

    bool canPlay = true;

    // Use this for initialization
    void Start () {
	
	}

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (canPlay)
        {
            canPlay = false;
            source.PlayOneShot(sound);
            yield return new WaitForSeconds(2);
            canPlay = true;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
