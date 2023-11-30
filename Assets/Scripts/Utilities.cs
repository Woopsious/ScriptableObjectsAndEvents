using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Utilities
{
	public static int GetRandomNumber(int num)
	{
		return Random.Range(0, num);
	}
	public static Vector3 GetRandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z));
	}
}
