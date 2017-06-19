using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
namespace LoadingControl{

	public class Item {

		private const string LANGUAGE_PATH = "items";

		public static string key = "key";
		public static string key_chinese = "zh-tw";
		public static string key_chinese_desc = "zh-tw desc";
		public static string key_english = "en";
		public static string key_english_desc = "en desc";
		public static string key_unlock_level = "unlock_lv";

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
		public static string 

		//material sheet
		public static string Type_Materials = "Materials";
		public static string Type_Weapon = "Weapon";

		private static Dictionary<string, object> itemTable;

		private static Dictionary<K, V> ToDictionary<K, V> (Hashtable table)
		{
			return table.Cast<DictionaryEntry>().ToDictionary(kvp =>(K)kvp.Key, kvp => (V)kvp.Value);
		}

		public static void LoadItem()
		{
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

		public static string SearchResultItem(string parent, string item_1, string item_2, string item_3)
		{
			bool foundItem = false;
			string resultItem = "";
			if(itemTable != null && itemTable.Count > 0)
			{
				Hashtable parentTable = (Hashtable)itemTable[parent];
				foreach(string key in parentTable.Keys)
				{
					Hashtable childTable = (Hashtable)parentTable[key];
					foundItem = ((string)childTable[weapon_key_param1] == item_1 
						&& (string)childTable[weapon_key_param2] == item_2 
						&& (string)childTable[weapon_key_param3] == item_3);
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
				componentItem.Add (key_param1, (string)childTable [weapon_key_param1]);
				componentItem.Add (key_param1count, (string)childTable [weapon_key_param1count]);
				componentItem.Add (key_param2, (string)childTable [weapon_key_param2]);				
				componentItem.Add (key_param2count, (string)childTable [weapon_key_param2count]);
				componentItem.Add (key_param3, (string)childTable [weapon_key_param3]);
				componentItem.Add (key_param3count, (string)childTable [weapon_key_param3count]);
				componentItem.Add (key_param4, (string)childTable [weapon_key_param4]);
				componentItem.Add (key_param4count, (string)childTable [weapon_key_param4count]);
				return componentItem;
			} else {
				return null;
			}
		}
	}
}

public enum weaponType{weapon,armor,item}
public enum career{all}

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
}
public class WeaponInfo{
	public string weaponkey;
	public weaponType type;
	public int atk;
	public int count;
	public string Name;
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
}