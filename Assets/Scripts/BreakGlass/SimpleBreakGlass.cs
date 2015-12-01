using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleBreakGlass : MonoBehaviour {
	public List<GameObject> BrokenGlassGO; // The broken glass GameObject
	GameObject BrokenGlassInstance; 
	public bool BreakSound=true;
	public GameObject SoundEmitter; //An object that will emit sound
	public float SoundEmitterLifetime=2.0f;
	public float ShardsLifetime=3.0f; //Lifetime of shards in seconds (0 if you don't want shards to disappear)
	public float ShardMass=0.5f; //Mass of each shard
	public Material ShardMaterial;
	public LayerMask usedLayers = ~0;
	private bool isUsed = false;
	
	void OnTriggerEnter(Collider other)
	{
		if (isUsed)
						return;
				else
						isUsed = true;
		if (((1<<other.gameObject.layer) & usedLayers) != 0) {
						BrokenGlassInstance = Instantiate (BrokenGlassGO [Random.Range (0, BrokenGlassGO.Count)], transform.position, transform.rotation) as GameObject;
		
						BrokenGlassInstance.transform.localScale = transform.lossyScale;
		
						foreach (Transform t in BrokenGlassInstance.transform) {
								t.GetComponent<Renderer> ().material = ShardMaterial;
								t.GetComponent<Rigidbody> ().mass = ShardMass;
						}
		
						if (BreakSound)
								Destroy (Instantiate (SoundEmitter, transform.position, transform.rotation) as GameObject, SoundEmitterLifetime);
		
						if (ShardsLifetime > 0)
								Destroy (BrokenGlassInstance, ShardsLifetime);
						Destroy (gameObject);
				}
	}
}
