using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum monstereffect{
	none,//0
	attack,//普攻100% 1
	roar,//怒吼  傷害增加20%兩回合 2
	regenhp,//回血  補血20% 3
	rock,//巨石 全體傷害50% 4
	scorpionvenom,//蠍毒 隊伍隨機中毒 5
	strongpunch,//重擊 全體攻擊傷害50% 6
	throwbomb,//丟炸彈 全體攻擊傷害50% 7
	summonroar,//咆哮 招喚野狼1，死亡後消失 8
	corrosivecharge,//毒疫彈  隨機1人每-10hp/10s持續1分鐘 9
	dive,//俯衝  基礎攻擊120% 10
	summonskeleton,//招喚骷髏怪  招喚骷髏怪1，死亡後消失 11
	summonwoodspirit,//招喚木精靈  招喚木精靈1，死亡後消失 12
	sheep,//變綿羊  隊伍隨機1人變綿羊2回合 13
	rockskin,//石膚強化  防禦率+50、100%抗暴 兩回合 14
	watergun,//水槍攻擊  基礎攻擊130% 15
	endlesscut,//滅世斬  全體攻擊100% 16
	firedragonbreathe,//火龍吐熄  全體攻擊100% 17
	dragonsroar,//龍之咆嘯  傷害增加20%兩回合 18
	devilsbound,//深淵束縛  隊伍隨機無法攻擊2回合 19
	Arcanemissile,//秘術彈  基礎攻擊130% 20
	shieldofspirit,//精靈的加護  補血20% 21
	storm//暴風  全體攻擊傷害100% 22
}

public enum MissionPointType{
	Resource,Monster
}
/**
怪物表與資源表的共用
*/
public class MissionPoint{
	public string key;
	public MissionPointType type;
	public string zh_tw;
	public string en;
	public int lv;
	public int hp;
	public int attack;
	public int exp;
	public Dictionary<monstereffect,int> effect;//最多三個效果，最少一個，如果是restype就無
	public Dictionary<string,int> drop;//三種寶物掉落，最後為不掉落
	public string animationpicturename;
	public int animationpicturecount;

	public static monstereffect Getmonstereffect(int value){
		return (monstereffect)Enum.ToObject(typeof(monstereffect) , value);
	}


}
