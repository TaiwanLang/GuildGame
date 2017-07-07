using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum equipmentType{weapon,armor,item,cannotread}  //裝備類型
public enum career{all,cannotread} //職業

public class EquipmentInfo{
	public string equipmentkey;		//裝備的key名，用這個對應
	public equipmentType type;		//裝備種類，目前有三種 武器 道具 防具
	public int count;				//個數
	public string tw;				//中文名
	public string tw_desc;			//中文說明
	public string en;				//英文名
	public string en_desc;			//英文說明
	public string effect1;			//效果1
	public int effect1_count;		//效果1的數值
	public string effect2;			//效果2
	public int effect2_count;		//效果2的數值
	public career usablecareer;		//可使用的職業
	public int price_value;			//裝備基礎價值
	public bool unlocked;			//解鎖與否
	public int unlock_level;		//解鎖等級
	public string picture_name;		//圖片名
}