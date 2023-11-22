using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
	public List<GameObject> ListOfSpawnableEnemies = new List<GameObject>();
	public int maxNumOfSpawnableEnemies;
	public int numOfEnemiesCurrentlySpawned;

	public float spawnTimer;
	private float spawnCooldown = 5;

	public void Update()
	{
		if (numOfEnemiesCurrentlySpawned < maxNumOfSpawnableEnemies)
		{
			spawnTimer -= Time.deltaTime;

			if (spawnTimer < 0)
			{
				spawnTimer = spawnCooldown;
				SpawnEnemy();
			}
		}
	}
	public void SpawnEnemy()
	{
		GameObject go = Instantiate(ListOfSpawnableEnemies[GetRandomNumber(ListOfSpawnableEnemies.Count)], gameObject.transform);

		Debug.Log("name of spawned Enemy: " + go.name);
		OnEnemySpawn();
	}
	public int GetRandomNumber(int num)
	{
		return Random.Range(0, num);
	}

	public void OnEnemySpawn()
	{
		numOfEnemiesCurrentlySpawned++;
		Debug.Log("enemy Spawned");
	}
	public void OnEnemyDeath()
	{
		numOfEnemiesCurrentlySpawned--;
	}
}
