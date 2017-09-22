using UnityEngine;
using System.Collections;

public class Character
{
	public string name;
	public int hp;
	public int maxhp;
	public int level;
	public int nextLevel;
	public int exp;
	public int nextExp;
	public string category; //職業

	public Character()
	{
		name = "";
		level = -1;
		nextLevel = -1;
		exp = -1;
		nextExp = -1;
		category = "";
	}

	public Character(string _name, int _level, int _nextLevel, int _exp, int _nextExp,string _Category)
	{
		name = _name;
		level = _level;
		nextLevel = _nextLevel;
		exp = _exp;
		nextExp = _nextExp;
		category = _Category;
	}

	public void Hit(int attack){
		hp -= attack;
	}

	public void Exp_Add(int exp_addition){
		exp += exp_addition;
	}


}

