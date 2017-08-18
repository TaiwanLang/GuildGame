using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoadingControl;
public class AutoSave {
	private static string material_savingstring = "material";
	private static string equipment_savingstring = "equipment";
	private static string building_savingstring = "building";
	public static bool SaveGuildInfo(){
		return true;
	}

	public static bool SaveBuildingInfo(){
//		Dictionary<string,string> materialjson = new Dictionary<string, string>();
//
//		foreach (Building materials in LoadingControl.LoadingData.materialinfo_list) {
//			if(materials.count > 0 ){
//				materialjson.Add (materials.Name,materials.count+"");
//			}
//		}
//		string materialjsonstr = MiniJSON.jsonEncode (materialjson);
//		PlayerPrefs.SetString (building_savingstring,materialjsonstr);
		return true;
	}

	public static bool SaveMaterialInfo(){
		Dictionary<string,string> materialjson = new Dictionary<string, string>();

		foreach (GameMaterial materials in LoadingControl.LoadingData.materialinfo_list) {
			if(materials.count > 0 ){
				materialjson.Add (materials.itemkey,materials.count+"");
			}
		}
		string materialjsonstr = MiniJSON.jsonEncode (materialjson);
		PlayerPrefs.SetString (material_savingstring,materialjsonstr);
		return true;
	}

	public static bool SaveEquipmentInfo(){
		Dictionary<string,string> equipmentjson = new Dictionary<string, string>();

		foreach (Equipment equipment in LoadingControl.LoadingData.equipmentinfo_list) {
			if(equipment.count > 0 ){
				equipmentjson.Add (equipment.equipmentkey,equipment.count+"");
			}
		}
		string equipmentjsonstr = MiniJSON.jsonEncode (equipmentjson);
		PlayerPrefs.SetString (equipment_savingstring,equipmentjsonstr);
		return true;
	}

}
