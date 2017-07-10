using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
namespace LoadingControl{

	public class LoadingData {

		private const string LANGUAGE_PATH = "gamedatas";

		public const string table_equipment = "equipment";
		public const string table_material = "material";
		public const string table_blacksmith = "blacksmith";
		public const string table_stage = "stage";
		public const string table_alchemy = "alchemy";
		public const string table_missionpoint = "missionpoint";//也是怪物與掉落表

		public const string key = "key";
		public const string key_chinese = "zh-tw";
		public const string key_chinese_desc = "zh-tw desc";
		public const string key_english = "en";
		public const string key_english_desc = "en desc";
		public const string key_picture = "picturename";
		public const string key_value = "value";

		//blacksmithaddition sheet
		public const string key_blacksmithaddition_param1 = "param1";				//素材1
		public const string key_blacksmithaddition_param1count = "param1count";	//素材1需要的個數
		public const string key_blacksmithaddition_param2 ="param2";				//素材2
		public const string key_blacksmithaddition_param2count = "param2count";	//素材2需要的個數
		public const string key_blacksmithaddition_param3 = "param3";				//素材3
		public const string key_blacksmithaddition_param3count = "param3count";	//素材3需要的個數
		public const string key_blacksmithaddition_param4 = "param4";				//素材4
		public const string key_blacksmithaddition_param4count = "param4count";	//素材4需要的個數

		//missionpoint sheet
		public const string key_missionpoint_type = "type";
		public const string key_missionpoint_level = "lv";
		public const string key_missionpoint_healthpoint = "hp";
		public const string key_missionpoint_attack = "attack";
		public const string key_missionpoint_exp = "exp";
		public const string key_missionpoint_effect1 = "effect1";
		public const string key_missionpoint_effect1percent = "effect1percent";
		public const string key_missionpoint_effect2 = "effect2";
		public const string key_missionpoint_effect2percent = "effect2percent";
		public const string key_missionpoint_effect3 = "effect3";
		public const string key_missionpoint_effect3percent = "effect3percent";
		public const string key_missionpoint_drop1 = "drop1";
		public const string key_missionpoint_drop1percent = "drop1percent";
		public const string key_missionpoint_drop2 = "drop2";
		public const string key_missionpoint_drop2percent = "drop2percent";
		public const string key_missionpoint_drop3 = "drop3";
		public const string key_missionpoint_drop3percent = "drop3percent";
		public const string key_missionpoint_nonedroppercent = "nonedroppercent";
		public const string key_missionpoint_animationpicture = "animationpicture";
		public const string key_missionpoint_animationpictruecount = "animationpicturecount";

		//stage sheet
		public const string key_stage_stagelevel = "stagelv";
		public const string key_stage_maxwarriorcount = "maxwarrior";
		public const string key_stage_maxlevel = "maxlevel";
		public const string key_stage_stagename = "stage";
		public const string key_stage_checkpointcount = "checkpoint";
		public const string key_stage_resourcepoint1 = "respoint1";
		public const string key_stage_resourcepoint1showpercent = "res1percent";
		public const string key_stage_resourcepoint2 = "respoint2";
		public const string key_stage_resourcepoint2showpercent = "res2percent";
		public const string key_stage_monster1 = "m1";
		public const string key_stage_monster1showpercent = "m1percent";
		public const string key_stage_monster2 = "m2";
		public const string key_stage_monster2showpercent = "m2percent";
		public const string key_stage_monster3 = "m3";
		public const string key_stage_monster3showpercent = "m3percent";
		public const string key_stage_monster4 = "m4";
		public const string key_stage_monster4showpercent = "m4percent";
		public const string key_stage_kingname = "king";
		public const string key_stage_kingshowpercent = "kingpercent";
		public const string key_stage_unlocklevel = "unlock";

		//equipment sheet
		public const string key_equipment_type = "type";					//裝備種類
		public const string key_equipment_effect1 = "effect1";				//特效1
		public const string key_equipment_effect1count = "effect1count";	//特效1的數值
		public const string key_equipment_effect2 = "effect2";				//特效2
		public const string key_equipment_effect2count = "effect2count";	//特效2的數值
		public const string key_equipment_career = "usablecareer";			//可使用職業
		public const string key_equipment_picturename = "picturename";
		public const string key_equipment_thumbnailpicture = "thumbnailpicture";

		//material sheet
		public const string key_material_rate = "rate";//稀有度
		public const string key_material_droprate = "droprate";//掉寶率
		public const string key_material_thumbnailpicture = "thumbnailpicture";

		//alchemy sheet
		public const string key_alchemy_param1 = "param1";
		public const string key_alchemy_param1count = "param1count";
		public const string key_alchemy_param2 = "param2";
		public const string key_alchemy_param2count = "param2count";

		public static List<Material> materialinfo_list;
		public static List<Equipment> equipmentinfo_list;
		public static List<BlackSmithAddition> blacksmithadditioninfo_list;
		public static List<Alchemy> alchemyinfo_list;
		public static List<MissionPoint> missionpointinfo_list;
		public static List<Stage> stageinfo_list;

		private static Dictionary<string, object> itemTable;

		private static Dictionary<K, V> ToDictionary<K, V> (Hashtable table)
		{
			return table.Cast<DictionaryEntry>().ToDictionary(kvp =>(K)kvp.Key, kvp => (V)kvp.Value);
		}

		public static void LoadItem()
		{
			materialinfo_list = new List<Material> ();
			equipmentinfo_list = new List<Equipment> ();
			blacksmithadditioninfo_list = new List<BlackSmithAddition> ();
			missionpointinfo_list = new List<MissionPoint> ();
			stageinfo_list = new List<Stage> ();
			alchemyinfo_list = new List<Alchemy> ();

			itemTable = new Dictionary<string, object>();
			itemTable = ToDictionary<string, object>((Hashtable)MiniJSON.jsonDecode(((TextAsset)Resources.Load(LANGUAGE_PATH)).text));
			SetInitListForBlacksmith ();
			SetInitListForMaterial ();
			SetInitListForEquipment ();
			SetInitListForMissionPoint ();
			SetInitListForStage ();
			SetInitListForAlchemy ();
		}

		public static int GetTabCount(string parent)
		{
			if(itemTable != null && itemTable.Count > 0)
			{
				Hashtable parentTable = (Hashtable)itemTable[parent];
				return parentTable.Count;
			}
			else
			{
				return 0;
			}
		}

		public static Hashtable GetTabTable(string parent)
		{
			if(itemTable != null && itemTable.Count > 0)
			{
				Hashtable parentTable = (Hashtable)itemTable[parent];
				return parentTable;
			}
			else return null;
		}


		public static void SetInitListForBlacksmith(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_blacksmith)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_blacksmith];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					BlackSmithAddition current_blacksmithaddition = new BlackSmithAddition();
					current_blacksmithaddition.itemkey = stringkey;
					Dictionary<string,int> data = new Dictionary<string, int>();
					data = CheckIfKeyGotValue (data, currenttable, key_blacksmithaddition_param1, key_blacksmithaddition_param1count);
					data = CheckIfKeyGotValue (data, currenttable, key_blacksmithaddition_param2, key_blacksmithaddition_param2count);
					data = CheckIfKeyGotValue (data, currenttable, key_blacksmithaddition_param3, key_blacksmithaddition_param3count);
					data = CheckIfKeyGotValue (data, currenttable, key_blacksmithaddition_param4, key_blacksmithaddition_param4count);
					current_blacksmithaddition.param = data;

					Utilities.DebugLog ("Blacksmith now is "+stringkey);

					blacksmithadditioninfo_list.Add (current_blacksmithaddition);
				}
			}
		}

		public static BlackSmithAddition FindBlackSmithAdditionByKey(string addtionkey){
			if (blacksmithadditioninfo_list == null||blacksmithadditioninfo_list.Count == 0)
				return null;
			BlackSmithAddition rblacksmithaddition = null;
			foreach (BlackSmithAddition blacksmithaddition in blacksmithadditioninfo_list) {
				if (blacksmithaddition.itemkey.CompareTo (addtionkey) == 0) {
					rblacksmithaddition = blacksmithaddition;
					break;
				}
			}
			return rblacksmithaddition;
		}
		public static void SetInitListForEquipment(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_equipment)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_equipment];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					Equipment current_equipment = new Equipment();
					int count = 0;
					current_equipment.equipmentkey = stringkey;
					equipmentType equipmenttype = equipmentType.cannotread;
					switch (currenttable [key_equipment_type].ToString()) {
					case"weapon":
						equipmenttype = equipmentType.weapon;
						break;
					case"armor":
						equipmenttype = equipmentType.armor;
						break;
					case"item":
						equipmenttype = equipmentType.item;
						break;
					default:
						break;
					}
					current_equipment.type = equipmenttype;
					current_equipment.tw = Utilities.LoadString(currenttable [key_chinese],"");
					current_equipment.tw_desc = Utilities.LoadString(currenttable [key_chinese_desc],"");
					current_equipment.en = Utilities.LoadString(currenttable [key_english],"");
					current_equipment.en_desc = Utilities.LoadString(currenttable [key_english_desc],"");
					Dictionary<string,int> data = new Dictionary<string, int>();
					data = CheckIfKeyGotValue (data, currenttable, key_equipment_effect1, key_equipment_effect1count);
					data = CheckIfKeyGotValue (data, currenttable, key_equipment_effect2, key_equipment_effect2count);
					current_equipment.effect = data;
					current_equipment.picture_name = Utilities.LoadString(currenttable [key_picture],"");
					current_equipment.price_value = Utilities.LoadInt(currenttable [key_value],0);
					career career=career.cannotread;
					switch (Utilities.LoadString(currenttable [key_equipment_career],"")) {
					case"all":
						career = career.all;
						break;
					case"":
						break;
					default:
						break;
					}
					current_equipment.usablecareer = career;
					current_equipment.picture_name = Utilities.LoadString (currenttable[key_equipment_picturename],"");
					current_equipment.thumbnailpicture_name = Utilities.LoadString (currenttable[key_equipment_thumbnailpicture],"");
					Utilities.DebugLog ("Equipment now is "+stringkey);
					equipmentinfo_list.Add (current_equipment);
				}
			}
		}
		public static Equipment FindEquipmentInfoByKey(string equipmentkey){
			if (equipmentinfo_list == null||equipmentinfo_list.Count == 0)
				return null;
			Equipment rweapon = null;
			foreach (Equipment equip in equipmentinfo_list) {
				if (equip.equipmentkey.CompareTo (equipmentkey) == 0) {
					rweapon = equip;
					break;
				}
			}
			return rweapon;
		}
		public static void SetInitListForMaterial(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_material)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_material];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					Material current_material = new Material();
					current_material.itemkey = stringkey;
					current_material.rate = Utilities.LoadInt (currenttable[key_material_rate],0);
					current_material.droprate = Utilities.LoadInt (currenttable[key_material_droprate],0);
					current_material.price_value = Utilities.LoadInt (currenttable[key_value],0);
					current_material.en = Utilities.LoadString(currenttable[key_english],"");
					current_material.tw = Utilities.LoadString(currenttable [key_chinese],"");
					current_material.en_desc = Utilities.LoadString(currenttable [key_english_desc],"");
					current_material.tw_desc = Utilities.LoadString(currenttable [key_chinese_desc],"");
					current_material.thumbnailpicture_name = Utilities.LoadString(currenttable [key_material_thumbnailpicture],"");
					Utilities.DebugLog ("material now is "+stringkey);
					materialinfo_list.Add (current_material);
				}
			}
		}
		public static Material FindMaterialByKey(string materialkey){
			if (materialinfo_list == null||materialinfo_list.Count == 0)
				return null;
			Material rmaterial = null;
			foreach (Material material in materialinfo_list) {
				if (material.itemkey.CompareTo (materialkey) == 0) {
					rmaterial = material;
					break;
				}
			}
			return rmaterial;
		}

		public static void SetInitListForMissionPoint(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_missionpoint)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_missionpoint];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Utilities.DebugLog ("missionpoint init is "+stringkey);
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					MissionPoint current_missionpoint = new MissionPoint();
					current_missionpoint.key = stringkey;
					string missiontype = Utilities.LoadString (currenttable[key_missionpoint_type],"");
					MissionPointType missionpointtype = MissionPointType.Monster;
					switch (missiontype) {
					case "resource":
						missionpointtype = MissionPointType.Resource;
						break;
					case "monster":
						missionpointtype = MissionPointType.Monster;
						break;
					default:
						break;
					}
					current_missionpoint.type = missionpointtype;
					current_missionpoint.zh_tw = Utilities.LoadString (currenttable[key_chinese],"");
					current_missionpoint.en = Utilities.LoadString (currenttable [key_english], "");
					current_missionpoint.lv = Utilities.LoadInt (currenttable[key_missionpoint_level],0);
					current_missionpoint.hp = Utilities.LoadInt (currenttable [key_missionpoint_healthpoint], 0);
					current_missionpoint.attack = Utilities.LoadInt (currenttable [key_missionpoint_attack],0);
					current_missionpoint.exp = Utilities.LoadInt (currenttable [key_missionpoint_exp], 0);
					Dictionary<monstereffect,int> data = new Dictionary<monstereffect, int>();
					int effectname1 = Utilities.LoadInt (currenttable[key_missionpoint_effect1],0);
					int effectpercent1 = Utilities.LoadInt (currenttable[key_missionpoint_effect1percent],0);
					int effectname2 = Utilities.LoadInt (currenttable[key_missionpoint_effect2],0);
					int effectpercent2 = Utilities.LoadInt (currenttable[key_missionpoint_effect2percent],0);
					int effectname3 = Utilities.LoadInt (currenttable[key_missionpoint_effect3],0);
					int effectpercent3 = Utilities.LoadInt (currenttable[key_missionpoint_effect3percent],0);
					if (effectname1 != 0) {
						data.Add (MissionPoint.Getmonstereffect(effectname1),effectpercent1);
					}
					if (effectname2 != 0) {
						data.Add (MissionPoint.Getmonstereffect(effectname2),effectpercent2);
					}
					if (effectname3 != 0) {
						data.Add (MissionPoint.Getmonstereffect(effectname3),effectpercent3);
					}
					current_missionpoint.effect = data;
					Dictionary<string,int> dropdata = new Dictionary<string, int>();
					dropdata = CheckIfKeyGotValue (dropdata, currenttable, key_missionpoint_drop1, key_missionpoint_drop1percent);
					dropdata = CheckIfKeyGotValue (dropdata, currenttable, key_missionpoint_drop2, key_missionpoint_drop2percent);
					dropdata = CheckIfKeyGotValue (dropdata, currenttable, key_missionpoint_drop3, key_missionpoint_drop3percent);
					dropdata.Add ("nonedroppercent",Utilities.LoadInt(key_missionpoint_nonedroppercent,0));
					current_missionpoint.drop = dropdata;
					current_missionpoint.animationpicturename = Utilities.LoadString (currenttable[key_missionpoint_animationpicture]);
					current_missionpoint.animationpicturecount = Utilities.LoadInt (currenttable [key_missionpoint_animationpictruecount]);

					missionpointinfo_list.Add (current_missionpoint);
					Utilities.DebugLog ("missionpoint now finish add is "+stringkey);
				}
			}
		}

		public static MissionPoint FindMissionByKey(string missionpointkey){
			if (missionpointinfo_list == null||missionpointinfo_list.Count == 0)
				return null;
			MissionPoint rmissionpoint = null;
			foreach (MissionPoint missionpoint in missionpointinfo_list) {
				if (missionpoint.key.CompareTo (missionpointkey) == 0) {
					rmissionpoint = missionpoint;
					break;
				}
			}
			return rmissionpoint;
		}

		public static void SetInitListForStage(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_stage)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_stage];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					Stage current_stage = new Stage();
					current_stage.key = stringkey;
					current_stage.stagelv = Utilities.LoadInt (currenttable[key_stage_stagelevel],0);
					current_stage.maxwarrior = Utilities.LoadInt (currenttable[key_stage_maxwarriorcount],0);
					current_stage.maxlevel = Utilities.LoadInt (currenttable[key_stage_maxlevel],0);
					current_stage.stage = Utilities.LoadString (currenttable[key_stage_stagename],"");
					current_stage.checkpointcount = Utilities.LoadInt (currenttable[key_stage_checkpointcount],0);
					current_stage.checkpointdata = new Dictionary<string, int> ();
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_resourcepoint1, key_stage_resourcepoint1showpercent);
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_resourcepoint2, key_stage_resourcepoint2showpercent);
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_monster1, key_stage_monster1showpercent);
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_monster2, key_stage_monster2showpercent);
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_monster3, key_stage_monster3showpercent);
					current_stage.checkpointdata = CheckIfKeyGotValue (current_stage.checkpointdata, currenttable,
						key_stage_monster4, key_stage_monster4showpercent);
					
					current_stage.kingname = Utilities.LoadString (currenttable[key_stage_kingname],"");
					current_stage.kingpercent = Utilities.LoadInt (currenttable[key_stage_kingshowpercent],0);
					string guildlevel = Utilities.LoadString (currenttable[key_stage_unlocklevel],"");
					guildlevel = guildlevel.Replace ("guild lv", "");
					current_stage.unlock_onguildlevel = Utilities.LoadInt (guildlevel,0);
					Utilities.DebugLog ("stage now is "+stringkey);
					stageinfo_list.Add (current_stage);
				}
			}
		}

		public static Stage FindStageByKey(string stagekey){
			if (stageinfo_list == null||stageinfo_list.Count == 0)
				return null;
			Stage rstage = null;
			foreach (Stage stage in stageinfo_list) {
				if (stage.key.CompareTo (stagekey) == 0) {
					rstage = stage;
					break;
				}
			}
			return rstage;
		}

		public static void SetInitListForAlchemy(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_alchemy)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_alchemy];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					Alchemy current_alchemy = new Alchemy();
					current_alchemy.itemkey = stringkey;
					current_alchemy.param = new Dictionary<Material, int> ();
					Material firstmaterial =  FindMaterialByKey (Utilities.LoadString(currenttable[key_alchemy_param1],""));
					if (firstmaterial == null) {
						Utilities.DebugLog ("alchemy can't find in material "+Utilities.LoadString(currenttable[key_alchemy_param1]));
						return;
					}
					current_alchemy.param.Add (firstmaterial,Utilities.LoadInt(currenttable[key_alchemy_param1count],0));
					string checking = Utilities.LoadString(currenttable[key_alchemy_param2],"");
					if (checking != null && checking.CompareTo ("") != 0) {
						Material secondmaterial =  FindMaterialByKey (Utilities.LoadString(currenttable[key_alchemy_param2],""));
						if (secondmaterial == null) {
							Utilities.DebugLog ("alchemy can't find in material "+Utilities.LoadString(currenttable[key_alchemy_param2]));
							return;
						}
						current_alchemy.param.Add (secondmaterial,Utilities.LoadInt(currenttable[key_alchemy_param2count],0));
					}

					Utilities.DebugLog ("alchemy now is "+stringkey);
					alchemyinfo_list.Add (current_alchemy);
				}
			}
		}

		public static Alchemy FindAlchemyByKey(string alchemykey){
			if (alchemyinfo_list == null||alchemyinfo_list.Count == 0)
				return null;
			Alchemy ralchemy = null;
			foreach (Alchemy alchemy in alchemyinfo_list) {
				if (alchemy.itemkey.CompareTo (alchemykey) == 0) {
					ralchemy = alchemy;
					break;
				}
			}
			return ralchemy;
		}

		private static Dictionary<string,int> CheckIfKeyGotValue(Dictionary<string,int> dic,Hashtable table,string checkstring,string checkcount){
			string checking = Utilities.LoadString (table[checkstring],"");
			if (checking != null && checking.CompareTo ("") != 0) {
				dic.Add (checking,Utilities.LoadInt(table[checkcount],0));
			}
			return dic;
		}
	}
}
