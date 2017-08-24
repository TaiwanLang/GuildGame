using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildCharaterController : MonoBehaviour {
	Animation anim;
	private string idle = "wait";
	private string attack = "attack";
	private string dead ="dead";
	private string magic = "magic";
	private string walk = "walk";
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		anim.Play (idle);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setWalkingLoop(){
		anim.Play (walk);

	}
	public float setattack(){
		Debug.Log ("attack called");
		anim.Play (attack);
		anim.PlayQueued (idle);
		return anim.GetClip (attack).length;
	}
}
