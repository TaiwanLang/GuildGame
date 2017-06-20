using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
namespace LoadingControl{

	public class Item {

		private const string LANGUAGE_PATH = "items";

		public static string table_weapon = "weapon";
		public static string table_material = "item";
		public static string table_addition = "addition";

		public static string key = "key";
		public static string key_chinese = "zh-tw";
		public static string key_chinese_desc = "zh-tw desc";
		public static string key_english = "en";
		public static string key_english_desc = "en desc";
		public static string key_picture = "picturename";
		public static string key_value = "value";

		//addition sheet
		public static string key_addition_param1 = "param1";
		public static string key_addition_param1count = "param1count";
		public static string key_addition_param2 ="param2";
		public static string key_addition_param2count = "param2count";
		public static string key_addition_param3 = "param3";
		public static string key_addition_param3count = "param3count";
		public static string key_addition_param4 = "param4";
		public static string key_addition_param4count = "param4count";

		//weapon sheet
		public static string key_weapon_attacknumber = "atk";
		public static string key_weapon_type = "type";
		public static string key_weapon_effect1 = "effect1";
		public static string key_weapon_effect1count = "effect1count";
		public static string key_weapon_effect2 = "effect2";
		public static string key_weapon_effect2count = "effect2count";
		public static string key_weapon_career = "usablecareer";

		//material sheet
		public static string key_item_rate = "rate";
		public static string key_item_droprate = "droprate";

		public static List<MaterialInfo> materialinfo_list;
		public static List<WeaponInfo> weaponinfo_list;
		public static List<AdditionInfo> additioninfo_list;

		private static Dictionary<string, object> itemTable;

		private static Dictionary<K, V> ToDictionary<K, V> (Hashtable table)
		{
			return table.Cast<DictionaryEntry>().ToDictionary(kvp =>(K)kvp.Key, kvp => (V)kvp.Value);
		}

		public static void LoadItem()
		{
			materialinfo_list = new List<MaterialInfo> ();
			weaponinfo_list = new List<WeaponInfo> ();
			additioninfo_list = new List<AdditionInfo> ();
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

		public static string SearchResultItem(string parent, string item_1, string item_2, string item_3,string item_4)
		{
			bool foundItem = false;
			string resultItem = "";
			if(itemTable != null && itemTable.Count > 0)
			{
				Hashtable parentTable = (Hashtable)itemTable[parent];
				foreach(string key in parentTable.Keys)
				{
					Hashtable childTable = (Hashtable)parentTable[key];
					foundItem = ((string)childTable[key_addition_param1] == item_1 
						&& (string)childTable[key_addition_param2] == item_2 
						&& (string)childTable[key_addition_param3] == item_3
						&& (string)childTable[key_addition_param4] == item_4);
					if(foundItem)
					{
						resultItem = key;
						break;
					}
				}
			}

			return resultItem;
		}

		public static Dictionary<string, string> GetDictWithResultItem (string parent, string resultItem)
		{
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (parent))
					return null;
				Hashtable parentTable = (Hashtable)itemTable [parent];
				if (!parentTable.ContainsKey (resultItem))
					return null;
				Hashtable childTable = (Hashtable)parentTable [resultItem];

				Dictionary<string, string> componentItem = new Dictionary<string, string> ();
				componentItem.Add (key, (string)childTable [key]);
				componentItem.Add (key_item_rate, (string)childTable [key_item_rate]);
				componentItem.Add (key_item_droprate, (string)childTable [key_item_droprate]);				
				componentItem.Add (key_value, (string)childTable [key_value]);

				return componentItem;
			} else {
				return null;
			}
		}

		public static void SetInitListForAddition(){
			Utilities.DebugLog ("addition init called");
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					Utilities.DebugLog ("returned");
					return;
				}
				Utilities.DebugLog ("get parent table");
				Hashtable parentTable = (Hashtable)itemTable [table_addition];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					AdditionInfo current_addition = new AdditionInfo();
					int count = 0;
					current_addition.itemkey = stringkey;
					current_addition.param = new Dictionary<string, int> ();

					int.TryParse ((string)currenttable [key_addition_param1count] ,out count);
					current_addition.param.Add((string)currenttable [key_addition_param1],count);
					string currentparam = (string)currenttable [key_addition_param2];
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						int.TryParse ((string)currenttable [key_addition_param2count], out count);
						current_addition.param.Add((string)currenttable [key_addition_param2],count);
					}

					currentparam = (string)currenttable [key_addition_param3];
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						int.TryParse ((string)currenttable [key_addition_param3count], out count);
						current_addition.param.Add((string)currenttable [key_addition_param3],count);
					}
					currentparam = (string)currenttable [key_addition_param4];
					if (currentparam != null && currentparam.CompareTo("") != 0) {
						int.TryParse ((string)currenttable [key_addition_param4count], out count);
						current_addition.param.Add((string)currenttable [key_addition_param4],count);
					}
					additioninfo_list.Add (current_addition);
				}
			}
		}
		public static AdditionInfo FindAdditionByKey(string addtionkey){
			if (additioninfo_list == null||additioninfo_list.Count == 0)
				return null;
			AdditionInfo raddition = null;
			foreach (AdditionInfo addition in additioninfo_list) {
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
					Utilities.DebugLog ("returned");
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_weapon];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					WeaponInfo current_weapon = new WeaponInfo();
					int count = 0;
					current_weapon.weaponkey = stringkey;
					int.TryParse((string)currenttable[key_weapon_attacknumber],out count);
					weaponType weapontype = weaponType.cannotread;
					switch (currenttable [key_weapon_type].ToString()) {
					case"weapon":
						weapontype = weaponType.weapon;
						break;
					case"armor":
						weapontype = weaponType.armor;
						break;
					case"item":
						weapontype = weaponType.item;
						break;
					default:
						break;
					}
					current_weapon.type = weapontype;
					current_weapon.tw = (string)currenttable [key_chinese];
					current_weapon.tw_desc = (string)currenttable [key_chinese_desc];
					current_weapon.en = (string)currenttable [key_english];
					current_weapon.en_desc = (string)currenttable [key_english_desc];
					current_weapon.effect1 = (string)currenttable [key_weapon_effect1];
					current_weapon.effect1_count = parseInt((string)currenttable [key_weapon_effect1count]);
					current_weapon.effect2 = (string)currenttable [key_weapon_effect2];
					current_weapon.effect2_count = parseInt((string)currenttable [key_weapon_effect2count]);
					current_weapon.picture_name = (string)currenttable [key_picture];
					current_weapon.price_value = parseInt((string)currenttable [key_value]);
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
					current_weapon.usablecareer = career;
					Utilities.DebugLog ("career is "+current_weapon.usablecareer);
					weaponinfo_list.Add (current_weapon);
				}
			}
		}
		public static WeaponInfo FindWeaponInfoByKey(string weaponkey){
			if (weaponinfo_list == null||weaponinfo_list.Count == 0)
				return null;
			WeaponInfo rweapon = null;
			foreach (WeaponInfo weapon in weaponinfo_list) {
				if (weapon.weaponkey.CompareTo (weaponkey) == 0) {
					rweapon = weapon;
					break;
				}
			}
			return rweapon;
		}
		public static void SetInitListForMaterial(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					Utilities.DebugLog ("returned");
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_material];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					MaterialInfo current_material = new MaterialInfo();
					current_material.itemkey = stringkey;
					current_material.rate = parseInt((string)currenttable[key_item_rate]);
					current_material.droprate = parseInt((string)currenttable[key_item_droprate]);
					current_material.price_value = parseInt((string)currenttable [key_value]);
					current_material.en = (string)currenttable[key_english];
					current_material.tw = (string)currenttable [key_chinese];
					current_material.en_desc = (string)currenttable [key_english_desc];
					current_material.tw_desc = (string)currenttable [key_chinese_desc];
					current_material.picture_name = (string)currenttable [key_picture];
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
		private static int parseInt(string parseingitem){
			int count = 0;

			int.TryParse (parseingitem, out count);

			return count;
		}
	}
}

public enum weaponType{weapon,armor,item,cannotread}  //裝備類型
public enum career{all,cannotread} //職業

public class AdditionInfo{
	public string itemkey;
	public Dictionary<string,int> param;
}
public class MaterialInfo{
	public int index;
	public string itemkey;
	public int count;
	public int rate;
	public int droprate;
	public int price_value;
	public string tw;
	public string tw_desc;
	public string en;
	public string en_desc;
	public string Name;
	public int unlock_lv;
	public bool unlocked;
	public string picture_name;
}
public class WeaponInfo{
	public string weaponkey;
	public weaponType type;
	public int count;
	public string tw;
	public string tw_desc;
	public string en;
	public string en_desc;
	public string effect1;
	public int effect1_count;
	public string effect2;
	public int effect2_count;
	public career usablecareer;
	public int price_value;
	public bool unlocked;
	public int unlock_level;	
	public string picture_name;
}