using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>建築物基底Class 遊戲各種類型建築物將會從此繼承</summary>

public class Building {

	public string name;
	public int level;
	public int nextLevel;
	public int exp;
	public int nextExp;

	public bool hasCharacter;
	public int count;
	public int maxCount;

	public Building()
	{
		name = "";
		level = -1;
		nextLevel = -1;
		exp = -1;
		nextExp = -1;

		hasCharacter = false;
		count = -1;
		maxCount = -1;
	}

	public Building(string _name, int _level, int _nextLevel, int _exp, int _nextExp, bool _hasCharacter, int _count, int _maxCount)
	{
		name = _name;
		level = _level;
		nextLevel = _nextLevel;
		exp = _exp;
		nextExp = _nextExp;

		hasCharacter = _hasCharacter;
		count = _count;
		maxCount = _maxCount;
	}
}
