using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armors : Items
{
	[Header("Armor Info")]
	public int bonusHealth;
	public int bonusMana;

	[Header("Armor Slot")]
	public ArmorSlot armorSlot;
	public enum ArmorSlot
	{
		helmet, chestpiece, legs, robe
	}

	[Header("Resistances")]
	public int bonusPhysicalResistance;
	public int bonusPoisonResistance;
	public int bonusFireResistance;
	public int bonusIceResistance;

	public override void Start()
	{
		base.Start();
		SetArmorStats();
		isStackable = false;
	}
	public void SetArmorStats()
	{
		bonusHealth = (int)(armorBaseRef.baseBonusHealth * statModifier);
		bonusMana = (int)(armorBaseRef.baseBonusMana * statModifier);

		armorSlot = (ArmorSlot)armorBaseRef.armorSlot; // may not need this depending on what happens when i make proper inventroy

		bonusPhysicalResistance = (int)(armorBaseRef.bonusPhysicalResistance * statModifier);
		bonusPoisonResistance = (int)(armorBaseRef.bonusPoisonResistance * statModifier);
		bonusFireResistance = (int)(armorBaseRef.bonusFireResistance * statModifier);
		bonusIceResistance = (int)(armorBaseRef.bonusIceResistance * statModifier);
	}
}
