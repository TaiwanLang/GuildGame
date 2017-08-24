using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRotate : MonoBehaviour {
	float zrotate = 0.1f;

	void Start () {
		
	}

	void Update () {
		transform.Rotate (0, 0, zrotate);
	}
}
