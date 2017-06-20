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

		//material sheet
		public static string key_item_rate = "rate";
		public static string key_item_droprate = "droprate";
		public static string key_item_value = "value";

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
				componentItem.Add (key_item_value, (string)childTable [key_item_value]);

				return componentItem;
			} else {
				return null;
			}
		}
		public static void GetDicForAddition(){
			Utilities.DebugLog ("addition init called");
			if (itemTable != null && itemTable.Count > 0) {
				if (!itemTable.ContainsKey (table_addition)) {
					Utilities.DebugLog ("returned");
					return;
				}
				Utilities.DebugLog ("get parent table");

				Hashtable parentTable = (Hashtable)itemTable [table_addition];
				foreach (Hashtable childtable in parentTable) {
					//把每一行抓出來
					Hashtable currenttable = (Hashtable)childtable;
					AdditionInfo current_addition = new AdditionInfo();
					int count = 0;

					current_addition.itemkey = (string)currenttable [key];
					current_addition.param = new Dictionary<string, int> ();

					int.TryParse ((string)currenttable [key_addition_param1count] ,out count);
					current_addition.param.Add((string)currenttable [key_addition_param1],count);
					if (!(currenttable [key_addition_param2]).Equals ("")) {
						int.TryParse ((string)currenttable [key_addition_param2count], out count);
						current_addition.param.Add((string)currenttable [key_addition_param2],count);
						Utilities.DebugLog ("now is " + count);
					}

					additioninfo_list.Add (current_addition);
					Utilities.DebugLog (current_addition.itemkey);
				}
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