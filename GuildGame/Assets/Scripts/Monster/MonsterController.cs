using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
	// Use this for initialization
	UISpriteAnimation monsteranimation;
	int animationcount;
	public int mostermaxani;
	public GameObject obj;
	void Start () {
		animationcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		monsteranimation = this.GetComponent (UISpriteAnimation);
		if (animationcount == mostermaxani) {
			monsteranimation.namePrefix = "";
		}
	}

}
