using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

	public int gridsize = 10;
	public static int bounds = 10;
	public int asteroidSpeed = 1;
	public int asteroidsPerRow = 8;


	public GameObject Inv;
	public int InvProbs = 40;
	public GameObject Spd;
	public int SpdProbs = 30;

	int loop = 0;
	bool hasDestroyed = false;

	public GameObject asteroid;

	List<List<GameObject>> asteroids = new List<List<GameObject>>();
	List<GameObject> newLayer = new List<GameObject> ();

	System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
		CreateLayer (1);
		CreateLayer (1.25);
		CreateLayer (1.5);
		CreateLayer (1.75);
		CreateLayer (2);
		CreateLayer (2.25);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hasDestroyed) {
			CreateLayer (1);
			hasDestroyed = false;
		}

		for (int j = 0; j < asteroids.Count; j++) {
			if (asteroids [j].Count > 0) {
				for (int x = 0; x < asteroids [j].Count; x++) {
					asteroids [j] [x].transform.Translate (new Vector3 (0, 0,-asteroidSpeed));
				}
				if (asteroids [j] [0].transform.position.z < -15 * gridsize) {
					Inv.active = true;
					Spd.active = true;
					DestroyLayer (asteroids [j]);
					asteroids.RemoveAt (j);
				}
			}
		}
	}

	void CreateLayer(double delay){
		newLayer = new List<GameObject> ();
		System.Random r = new System.Random ();
		int chanceInv = r.Next (0, InvProbs);
		int chanceSpd = r.Next (0, SpdProbs);

		if (chanceInv == InvProbs - 1) {
			newLayer.Add (Instantiate (Inv, new Vector3 (rand.Next (0, bounds + 1) * gridsize, rand.Next (0, bounds + 1) * gridsize, 30 * gridsize * (float)delay), Inv.transform.rotation));
		} else {
			for (int i = 0; i < asteroidsPerRow; i++) {
				newLayer.Add (Instantiate (asteroid, new Vector3 (rand.Next (0, bounds + 1) * gridsize, rand.Next (0, bounds + 1) * gridsize, 30 * gridsize * (float)delay), asteroid.transform.rotation));

			}
		}

		if (chanceSpd == SpdProbs - 1) {
			newLayer.Add (Instantiate (Spd, new Vector3 (rand.Next (0, bounds + 1) * gridsize, rand.Next (0, bounds + 1) * gridsize, 30 * gridsize * (float)delay), Spd.transform.rotation));
		} else {
			for (int i = 0; i < asteroidsPerRow; i++) {
				newLayer.Add (Instantiate (asteroid, new Vector3 (rand.Next (0, bounds + 1) * gridsize, rand.Next (0, bounds + 1) * gridsize, 30 * gridsize * (float)delay), asteroid.transform.rotation));
			}		
		}

		asteroids.Add (newLayer);
	}

	void DestroyLayer(List<GameObject> layer){
		for (int i = 0; i < layer.Count; i++) {
			Destroy (layer [i]);
		}
		hasDestroyed = true;
	}
}
