using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LoadingControl{

	public class GameControl : MonoBehaviour {
		private string addtionfindkey = "wood";
		private string weaponfindkey = "fairy ring";
		private string materialfindkey = "rags";
		// Use this for initialization
		void Start () {
			Item.LoadItem ();
			Item.SetInitListForAddition ();
			Item.SetInitListForMaterial ();
			Item.SetInitListForWeapon ();
			AdditionInfo getback = Item.FindAdditionByKey (addtionfindkey);
			EquipmentInfo getback2 = Item.FindWeaponInfoByKey (weaponfindkey);
			MaterialInfo getback3 = Item.FindMaterialByKey (materialfindkey);

			Utilities.DebugLog ("addition info "+getback.itemkey );
			Utilities.DebugLog ("need add "+getback.param.Count+"  item");
			foreach (KeyValuePair<string,int> param in getback.param) {
				Utilities.DebugLog ("addition key "+param.Key );
				Utilities.DebugLog ("addition value "+param.Value );
			}

			Utilities.DebugLog ("weapon info "+getback2.equipmentkey );

			Utilities.DebugLog ("weapon type "+getback2.type);

			Utilities.DebugLog ("weapon price "+getback2.price_value );

			Utilities.DebugLog ("material info "+getback3.itemkey );

			Utilities.DebugLog ("weapon tw "+getback3.tw);

			Utilities.DebugLog ("weapon desc "+getback2.tw_desc );

		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}