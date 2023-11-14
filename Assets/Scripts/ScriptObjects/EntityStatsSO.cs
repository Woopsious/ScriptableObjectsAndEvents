using UnityEngine;

[CreateAssetMenu(fileName = "EntityStatsScriptableObject", menuName = "EntityStats")]
public class EntityStatsSO : ScriptableObject
{
	public string entityName;

	[Header("health")]
	public int maxHealth;

	[Header("Resistances")]
	public int physicalDamageResistance;
	public int poisonDamageResistance;
	public int fireDamageResistance;
	public int iceDamageResistance;

	[Header("Speed")]
	public int moveSpeed;
	public int turnSpeed;

	[Header("Mana")]
	public int maxMana;
}
