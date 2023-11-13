using UnityEngine;

[CreateAssetMenu(fileName = "EntityStatsScriptableObject", menuName = "EntityStats")]
public class BaseStatsSO : ScriptableObject
{
	public int health;

	public int physicalDamageResistance;
	public int poisonDamageResistance;
	public int fireDamageResistance;
	public int iceDamageResistance;

	public int moveSpeed;
	public int mana;
}
