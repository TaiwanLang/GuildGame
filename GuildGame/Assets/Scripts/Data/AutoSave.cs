using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingControl;
public class AutoSave {

	public static bool SaveGuildInfo(){
		return false;
	}

	public static bool SaveBuildingInfo(){
		return false;
	}

	public static bool SaveMaterialInfo(){
		Dictionary<string,string> materialjson = new Dictionary<string, string>();

		foreach (GameMaterial materials in LoadingControl.LoadingData.materialinfo_list) {
			if(materials.count > 0 ){
				materialjson.Add (materials.Name,materials.count+"");
			}
		}
		string materialjsonstr = MiniJSON.jsonEncode (materialjson);
		PlayerPrefs.SetString ("material",materialjsonstr);
		return false;
	}

	public static bool SaveEquipmentInfo(){
		return false;
	}

}
