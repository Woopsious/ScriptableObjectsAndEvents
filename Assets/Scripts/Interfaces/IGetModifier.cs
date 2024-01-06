using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetStatModifier
{
	enum Rarity
	{
		isCommon, isRare, isLegendary
	}
	void GetStatModifier(int thingsLevel, Rarity rarity);
}
