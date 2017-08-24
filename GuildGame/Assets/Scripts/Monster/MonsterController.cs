using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
	// Use this for initialization
	UISpriteAnimation monsteranimation;
	int animationcount;
	public string attack;
	public string wait;
	void Start () {
		animationcount = 0;
		if(monsteranimation == null)
			monsteranimation = this.GetComponent<UISpriteAnimation> ();
		//StartCoroutine (StartAttack());
		monsteranimation.namePrefix = wait;
		monsteranimation.loop = true;
		monsteranimation.Play ();
	}

	IEnumerator AttackAndWait()
	{
		monsteranimation.namePrefix = attack;
		monsteranimation.Play ();
		float waitanimationtime = (float)monsteranimation.frames / (float)monsteranimation.framesPerSecond;
		print ("time is "+waitanimationtime);
		yield return new WaitForSeconds(waitanimationtime);
		monsteranimation.namePrefix = wait;
		monsteranimation.loop = true;
		monsteranimation.Play ();
	}
	public void SlimeAttack(){
		Debug.Log ("slime attack");
		StartCoroutine (StartAttack());
	}
	IEnumerator StartAttack()
	{
		yield return StartCoroutine("AttackAndWait");
	}

	// Update is called once per frame
	void Update () {
		
	}

}
