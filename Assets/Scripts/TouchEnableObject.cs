using UnityEngine;
using System.Collections;

public class TouchEnableObject : MonoBehaviour {
    public bool on = false;
    public GameObject ToDisable;
    public AudioSource audioSource = null;
    public Material on_texture;
    public Material off_texture;
    public AudioClip sound = null;

    private float fireRate = 1.0f;
    private float nextFire = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (on)
            {
                GetComponent<Renderer>().material = off_texture;
                if (sound == null)
                    audioSource.Stop();
                on = false;
            }
            else
            {
                GetComponent<Renderer>().material = on_texture;
                if (sound == null)
                    audioSource.Play();
                else
                    audioSource.PlayOneShot(sound);
                on = true;
            }
            ToDisable.SetActive(on);
        }
    }
	// Use this for initialization
	void Start () {
        ToDisable.SetActive(on);
	}
}
