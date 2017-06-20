using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LoadingControl{

	public class GameControl : MonoBehaviour {

		// Use this for initialization
		void Start () {
			Item.LoadItem ();
			int tablecount = Item.GetTabCount (Item.table_addition);
			Utilities.DebugLog ("try get addition");
			Item.GetDicForAddition ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}