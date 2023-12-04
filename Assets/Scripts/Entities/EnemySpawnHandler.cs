using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
	public List<GameObject> ListOfSpawnableEnemies = new List<GameObject>();
	public int maxNumOfSpawnableEnemies;
	public int numOfEnemiesCurrentlySpawned;

	public float spawnTimer;
	private float spawnCooldown = 5;

	public int spawnerLevel;

	public void Start()
	{
		//in MP change to array check for then get host level
		spawnerLevel = FindObjectOfType<PlayerController>().GetComponent<EntityHealth>().entityLevel;
	}

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
		GameObject go = Instantiate(ListOfSpawnableEnemies[Utilities.GetRandomNumber(ListOfSpawnableEnemies.Count)], 
			transform.position, Quaternion.identity);

		EntityHealth entityHealthRef = go.GetComponent<EntityHealth>();
		entityHealthRef.entityLevel = spawnerLevel;
		entityHealthRef.onDeathEvent.AddListener(delegate { OnEntityDeathEvent(); } );

		OnEnemySpawn();
	}

	public void OnEnemySpawn()
	{
		numOfEnemiesCurrentlySpawned++;
	}
	public void OnEntityDeathEvent()
	{
		numOfEnemiesCurrentlySpawned--;
	}
}
