using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace LoadingControl{

	public class GameControl : MonoBehaviour {
		private string blacksmithfindkey = "wand";
		private string equipmentfindkey = "bravers sword";
		private string materialfindkey = "rags";
		private string missionpointfindkey = "king firedragon lv40";
		// Use this for initialization
		void Start () {

			LoadingData.LoadItem ();
			LoadingData.SetInitListForBlacksmith ();
			LoadingData.SetInitListForMaterial ();
			LoadingData.SetInitListForEquipment ();
			LoadingData.SetInitListForMissionPoint ();
			Utilities.DebugLog ("LoadingData.GetTabCount "+LoadingData.GetTabCount (LoadingData.table_equipment));
			BlackSmithAddition getback = LoadingData.FindBlackSmithAdditionByKey (blacksmithfindkey);
			Equipment getback2 = LoadingData.FindEquipmentInfoByKey (equipmentfindkey);
			Material getback3 = LoadingData.FindMaterialByKey (materialfindkey);
			MissionPoint missionpointback = LoadingData.FindMissionByKey (missionpointfindkey);

			Utilities.DebugLog ("addition info "+getback.itemkey );
			Utilities.DebugLog ("need add "+getback.param.Count+"  item");
			foreach (KeyValuePair<string,int> param in getback.param) {
				Utilities.DebugLog ("addition key "+param.Key+"  value "+param.Value );
			}
			Utilities.DebugLog ("================================");
			Utilities.DebugLog ("weapon info "+getback2.equipmentkey );

			Utilities.DebugLog ("weapon type "+getback2.type);

			Utilities.DebugLog ("weapon price "+getback2.price_value );

			Utilities.DebugLog ("material info "+getback3.itemkey );

			Utilities.DebugLog ("weapon tw "+getback3.tw);

			Utilities.DebugLog ("weapon desc "+getback2.tw_desc );

			Utilities.DebugLog ("mission point now is "+missionpointback.key);
			Utilities.DebugLog ("mission point now is "+missionpointback.key);
			Utilities.DebugLog ("mission point now is "+missionpointback.key);
			Utilities.DebugLog ("mission point now is "+missionpointback.key);
			Utilities.DebugLog ("mission point now is "+missionpointback.key);

		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}