using UnityEngine;
using System.Collections;

public class Character
{
	public string name;
	public int level;
	public int nextLevel;
	public int exp;
	public int nextExp;


	public Character()
	{
		name = "";
		level = -1;
		nextLevel = -1;
		exp = -1;
		nextExp = -1;

	}

	public Character(string _name, int _level, int _nextLevel, int _exp, int _nextExp)
	{
		name = _name;
		level = _level;
		nextLevel = _nextLevel;
		exp = _exp;
		nextExp = _nextExp;

	}
}

