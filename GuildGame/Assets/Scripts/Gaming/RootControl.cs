using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootControl : MonoBehaviour {
	public GameObject MainScene;
	public GameObject FightScene;
	public bool MainSceneActive;
	public GameObject backswitch;
	public GameObject ToFight;
	private Dictionary<int,GameObject> fightpeople_dic;
	private Dictionary<int,GameObject> monster_dic;

	void Start () {
		monster_dic = new Dictionary<int, GameObject> ();
		fightpeople_dic = new Dictionary<int, GameObject> ();
		FightScene.SetActive (!MainSceneActive);
		MainScene.SetActive (MainSceneActive);
		GameObject slime = Instantiate(Resources.Load("slime", typeof(GameObject))) as GameObject;
		slime.transform.SetParent( FightScene.transform,false);
		monster_dic.Add (1,slime);
		slime.SetActive (true);
		GameObject people = Instantiate (Resources.Load ("FightPeople",typeof(GameObject))) as GameObject;
		fightpeople_dic.Add (1, people);

		people.transform.SetParent( FightScene.transform,false);
		people.SetActive (true);
		UIEventListener.Get (backswitch).onClick += BackToMain;
		UIEventListener.Get (ToFight).onClick += GoToFight;
	}
	private void BackToMain(GameObject game){
		print ("back to main");
		FightScene.SetActive (false);
		MainScene.SetActive(true);
	}
	private void GoToFight(GameObject fight){
		Debug.Log ("go to fight");
		FightScene.SetActive (true);
		MainScene.SetActive (false);
	}
	void Update () {
		if (Input.GetKeyDown ("space")) {
			Debug.Log ("monster attack");
			GameObject monster;
			bool getok = monster_dic.TryGetValue (1, out monster);
			if (getok) {
				MonsterController monstercontrol = monster.GetComponentInChildren<MonsterController> ();
				if (monstercontrol != null)
					monstercontrol.SlimeAttack ();
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Debug.Log ("people attack");
			GameObject people;
			bool getok = fightpeople_dic.TryGetValue (1, out people);
			if (getok) {
				GuildCharaterController characer = people.GetComponentInChildren<GuildCharaterController> ();
				if (characer != null) {
					float backtime = characer.setattack ();
					Debug.Log ("need time is "+backtime);
				}
			}
		}else if (Input.GetKeyDown(KeyCode.Alpha2)){
			GameObject people;
			bool getok = fightpeople_dic.TryGetValue (1, out people);
			if (getok) {
				GuildCharaterController characer = people.GetComponentInChildren<GuildCharaterController> ();
				if (characer != null) {
					characer.setWalkingLoop ();
				}
			}
		}
	}

	public void StartBlackSmithing(){
		
	}
}
