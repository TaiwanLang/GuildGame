using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material{ 		//素材的數值表
	public int index;		
	public string itemkey;		//素材本身的key名，用這個對應
	public int count;			//素材有幾個
	public int rate;			//素材的稀有度
	public int droprate;		//素材的掉寶率
	public int price_value;		//素材的基礎價值
	public string tw;			//素材的中文名
	public string tw_desc;		//中文說明
	public string en;			//英文名
	public string en_desc;		//英文說明
	public string Name;			//本次顯示的名稱
	public int unlock_lv;		//解鎖等級
	public bool unlocked;		//解鎖與否
	public string thumbnailpicture_name;	//素材的圖片名
}
