using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
namespace LoadingControl{

	public class Item {

		private const string LANGUAGE_PATH = "items";

		public const string table_weapon = "weapon";
		public const string table_material = "item";
		public const string table_addition = "addition";

		public const string key = "key";
		public const string key_chinese = "zh-tw";
		public const string key_chinese_desc = "zh-tw desc";
		public const string key_english = "en";
		public const string key_english_desc = "en desc";
		public const string key_picture = "picturename";
		public const string key_value = "value";

		//addition sheet
		public const string key_addition_param1 = "param1";				//素材1
		public const string key_addition_param1count = "param1count";	//素材1需要的個數
		public const string key_addition_param2 ="param2";				//素材2
		public const string key_addition_param2count = "param2count";	//素材2需要的個數
		public const string key_addition_param3 = "param3";				//素材3
		public const string key_addition_param3count = "param3count";	//素材3需要的個數
		public const string key_addition_param4 = "param4";				//素材4
		public const string key_addition_param4count = "param4count";	//素材4需要的個數

		//equipment sheet
		public const string key_weapon_type = "type";					//裝備種類
		public const string key_weapon_effect1 = "effect1";				//特效1
		public const string key_weapon_effect1count = "effect1count";	//特效1的數值
		public const string key_weapon_effect2 = "effect2";				//特效2
		public const string key_weapon_effect2count = "effect2count";	//特效2的數值
		public const string key_weapon_career = "usablecareer";			//可使用職業

		//material sheet
		public const string key_item_rate = "rate";//稀有度
		public const string key_item_droprate = "droprate";//掉寶率

		public static List<MaterialInfo> materialinfo_list;
		public static List<EquipmentInfo> equipmentinfo_list;
		public static List<BlackSmithAddition> additioninfo_list;

		private static Dictionary<string, object> itemTable;

		private static Dictionary<K, V> ToDictionary<K, V> (Hashtable table)
		{
			return table.Cast<DictionaryEntry>().ToDictionary(kvp =>(K)kvp.Key, kvp => (V)kvp.Value);
		}

		public static void LoadItem()
		{
			materialinfo_list = new List<MaterialInfo> ();
			equipmentinfo_list = new List<EquipmentInfo> ();
			additioninfo_list = new List<BlackSmithAddition> ();
			itemTable = new Dictionary<string, object>();
			itemTable = ToDictionary<string, object>((Hashtable)MiniJSON.jsonDecode(((TextAsset)Resources.Load(LANGUAGE_PATH)).text));
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


		public static void SetInitListForAddition(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_addition];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					BlackSmithAddition current_addition = new BlackSmithAddition();
					current_addition.itemkey = stringkey;
					current_addition.param = new Dictionary<string, int> ();
					current_addition.param.Add( Utilities.LoadString(currenttable [key_addition_param1],"")
						,Utilities.LoadInt(currenttable [key_addition_param1count] ,0));
					string currentparam =  Utilities.LoadString(currenttable [key_addition_param2],"");
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						current_addition.param.Add(currentparam,Utilities.LoadInt(currenttable [key_addition_param2count] ,0));
					}
					currentparam = Utilities.LoadString(currenttable [key_addition_param3],"");
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						current_addition.param.Add(currentparam,Utilities.LoadInt(currenttable [key_addition_param3count] ,0));
					}
					currentparam = Utilities.LoadString(currenttable [key_addition_param4],"");
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						current_addition.param.Add(currentparam,Utilities.LoadInt(currenttable [key_addition_param4count] ,0));
					}
					additioninfo_list.Add (current_addition);
				}
			}
		}
		public static BlackSmithAddition FindAdditionByKey(string addtionkey){
			if (additioninfo_list == null||additioninfo_list.Count == 0)
				return null;
			BlackSmithAddition raddition = null;
			foreach (BlackSmithAddition addition in additioninfo_list) {
				if (addition.itemkey.CompareTo (addtionkey) == 0) {
					raddition = addition;
					break;
				}
			}
			return raddition;
		}
		public static void SetInitListForWeapon(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_weapon];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					EquipmentInfo current_equipment = new EquipmentInfo();
					int count = 0;
					current_equipment.equipmentkey = stringkey;
					equipmentType equipmenttype = equipmentType.cannotread;
					switch (currenttable [key_weapon_type].ToString()) {
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
					current_equipment.effect1 = Utilities.LoadString(currenttable [key_weapon_effect1],"");
					current_equipment.effect1_count = Utilities.LoadInt(currenttable [key_weapon_effect1count],0);
					current_equipment.effect2 = Utilities.LoadString(currenttable [key_weapon_effect2],"");
					current_equipment.effect2_count = Utilities.LoadInt(currenttable [key_weapon_effect2count],0);
					current_equipment.picture_name = Utilities.LoadString(currenttable [key_picture],"");
					current_equipment.price_value = Utilities.LoadInt(currenttable [key_value],0);
					career career=career.cannotread;
					switch (currenttable [key_weapon_career].ToString()) {
					case"all":
						career = career.all;
						break;
					case"":
						break;
					default:
						break;
					}
					current_equipment.usablecareer = career;
					Utilities.DebugLog ("career is "+current_equipment.usablecareer);
					equipmentinfo_list.Add (current_equipment);
				}
			}
		}
		public static EquipmentInfo FindWeaponInfoByKey(string equipmentkey){
			if (equipmentinfo_list == null||equipmentinfo_list.Count == 0)
				return null;
			EquipmentInfo rweapon = null;
			foreach (EquipmentInfo equip in equipmentinfo_list) {
				if (equip.equipmentkey.CompareTo (equipmentkey) == 0) {
					rweapon = equip;
					break;
				}
			}
			return rweapon;
		}
		public static void SetInitListForMaterial(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_material];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					MaterialInfo current_material = new MaterialInfo();
					current_material.itemkey = stringkey;
					current_material.rate = Utilities.LoadInt (currenttable[key_item_rate],0);
					current_material.droprate = Utilities.LoadInt (currenttable[key_item_droprate],0);
					current_material.price_value = Utilities.LoadInt (currenttable[key_value],0);
					current_material.en = Utilities.LoadString(currenttable[key_english],"");
					current_material.tw = Utilities.LoadString(currenttable [key_chinese],"");
					current_material.en_desc = Utilities.LoadString(currenttable [key_english_desc],"");
					current_material.tw_desc = Utilities.LoadString(currenttable [key_chinese_desc],"");
					current_material.picture_name = Utilities.LoadString(currenttable [key_picture],"");
					materialinfo_list.Add (current_material);
				}
			}
		}
		public static MaterialInfo FindMaterialByKey(string materialkey){
			if (materialinfo_list == null||materialinfo_list.Count == 0)
				return null;
			MaterialInfo rmaterial = null;
			foreach (MaterialInfo material in materialinfo_list) {
				if (material.itemkey.CompareTo (materialkey) == 0) {
					rmaterial = material;
					break;
				}
			}
			return rmaterial;
		}

	}
}
