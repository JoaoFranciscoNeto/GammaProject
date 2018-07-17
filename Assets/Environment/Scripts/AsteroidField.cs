using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour {

    public GameObject AsteroidGameObject;
    public int nAsteroids;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < nAsteroids; i++)
        {
            Vector3 pos = Vector3.Scale(Random.insideUnitSphere, new Vector3(150, 20, 150));
            Instantiate(AsteroidGameObject, pos, Random.rotation, transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
