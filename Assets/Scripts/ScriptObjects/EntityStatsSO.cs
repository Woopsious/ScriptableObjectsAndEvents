using UnityEngine;

[CreateAssetMenu(fileName = "EntityStatsScriptableObject", menuName = "EntityStats")]
public class EntityStatsSO : ScriptableObject
{
	[Header("health")]
	public int maxHealth;
	public int health;

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

	private void OnEnable()
	{
		maxHealth = health;
	}
}
