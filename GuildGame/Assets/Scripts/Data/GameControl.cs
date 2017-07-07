using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LoadingControl{

	public class GameControl : MonoBehaviour {
		private string blacksmithfindkey = "wand";
		private string equipmentfindkey = "fairy ring";
		private string materialfindkey = "rags";
		// Use this for initialization
		void Start () {
			LoadingData.LoadItem ();
			LoadingData.SetInitListForBlacksmith ();
			LoadingData.SetInitListForMaterial ();
			LoadingData.SetInitListForEquipment ();
			Utilities.DebugLog ("LoadingData.GetTabCount "+LoadingData.GetTabCount (LoadingData.table_equipment));
			BlackSmithAddition getback = LoadingData.FindBlackSmithAdditionByKey (blacksmithfindkey);
			EquipmentInfo getback2 = LoadingData.FindEquipmentInfoByKey (equipmentfindkey);
			MaterialInfo getback3 = LoadingData.FindMaterialByKey (materialfindkey);

			/*Utilities.DebugLog ("addition info "+getback.itemkey );
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

			Utilities.DebugLog ("weapon desc "+getback2.tw_desc );*/

		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}