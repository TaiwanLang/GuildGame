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
		public const string key_addition_param1 = "param1";
		public const string key_addition_param1count = "param1count";
		public const string key_addition_param2 ="param2";
		public const string key_addition_param2count = "param2count";
		public const string key_addition_param3 = "param3";
		public const string key_addition_param3count = "param3count";
		public const string key_addition_param4 = "param4";
		public const string key_addition_param4count = "param4count";

		//weapon sheet
		public const string key_weapon_attacknumber = "atk";
		public const string key_weapon_type = "type";
		public const string key_weapon_effect1 = "effect1";
		public const string key_weapon_effect1count = "effect1count";
		public const string key_weapon_effect2 = "effect2";
		public const string key_weapon_effect2count = "effect2count";
		public const string key_weapon_career = "usablecareer";

		//material sheet
		public const string key_item_rate = "rate";
		public const string key_item_droprate = "droprate";

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


		public static void SetInitListForAddition(){
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_addition];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					AdditionInfo current_addition = new AdditionInfo();
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
					return;
				}
				Hashtable parentTable = (Hashtable)itemTable [table_weapon];

				foreach (string stringkey in parentTable.Keys){
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)parentTable[stringkey];
					WeaponInfo current_weapon = new WeaponInfo();
					int count = 0;
					current_weapon.weaponkey = stringkey;
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
					current_weapon.tw = Utilities.LoadString(currenttable [key_chinese],"");
					current_weapon.tw_desc = Utilities.LoadString(currenttable [key_chinese_desc],"");
					current_weapon.en = Utilities.LoadString(currenttable [key_english],"");
					current_weapon.en_desc = Utilities.LoadString(currenttable [key_english_desc],"");
					current_weapon.effect1 = Utilities.LoadString(currenttable [key_weapon_effect1],"");
					current_weapon.effect1_count = Utilities.LoadInt(currenttable [key_weapon_effect1count],0);
					current_weapon.effect2 = Utilities.LoadString(currenttable [key_weapon_effect2],"");
					current_weapon.effect2_count = Utilities.LoadInt(currenttable [key_weapon_effect2count],0);
					current_weapon.picture_name = Utilities.LoadString(currenttable [key_picture],"");
					current_weapon.price_value = Utilities.LoadInt(currenttable [key_value],0);
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