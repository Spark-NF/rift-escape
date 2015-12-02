using UnityEngine;
using System.Collections;

public class TouchEnableObject : MonoBehaviour {
    public bool on = false;
    public GameObject ToDisable;
    public AudioSource audioSource = null;
    public AudioClip sound = null;

    bool canChange = true;

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (canChange)
        {
            canChange = false;
            if (on)
            {
                if (sound == null)
                    audioSource.Stop();
                on = false;
            }
            else
            {
                if (sound == null)
                    audioSource.Play();
                else
                    audioSource.PlayOneShot(sound);
                on = true;
            }
            ToDisable.SetActive(on);
            yield return new WaitForSeconds(2);
            canChange = true;
        }
    }
	// Use this for initialization
	void Start () {
        ToDisable.SetActive(on);
	}
}
