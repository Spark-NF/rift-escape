using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour {
	public bool on = false;
	public GameObject source;
	public Material on_texture;
	public Material off_texture;
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
                source.GetComponent<Renderer>().material = off_texture;
                if (sound == null)
                    audioSource.Stop();
                on = false;
            }
            else
            {
                source.GetComponent<Renderer>().material = on_texture;
                if (sound == null)
                    audioSource.Play();
                else
                    audioSource.PlayOneShot(sound);
                on = true;
            }
            yield return new WaitForSeconds(2);
            canChange = true;
        }
	}
}