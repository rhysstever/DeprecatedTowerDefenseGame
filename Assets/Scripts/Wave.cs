﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
	#region Fields
	private string name;
	private int numOfEnemies;
	private GameObject enemyPrefab;
	private float spawnDelay;
	private bool hasSpawned;
	private bool hasCleared;
	private int numSpawned;
	private int numLeft;
	private Wave nextWave;
	#endregion

	#region Properties
	public string Name { get { return name; } }
	public GameObject EnemyPrefab { get { return enemyPrefab; } }
	public int WaveCount { get { return numOfEnemies; } }
	public float SpawnDelay { get { return spawnDelay; } }
	public bool HasSpawned { get { return hasSpawned; } }
	public bool HasCleared { get { return hasCleared; } }
	public int EnemiesSpawned { get { return numSpawned; } }
	public int EnemiesLeft { get { return numLeft; } }
	public Wave NextWave { get { return nextWave; } }
	#endregion

	#region Contructor
	public Wave(string name, GameObject enemyPrefab, int numOfEnemies, float spawnDelay, Wave nextWave) : this(name, enemyPrefab, numOfEnemies, spawnDelay)
	{
		this.nextWave = nextWave;
	}
	public Wave(string name, GameObject enemyPrefab, int numOfEnemies, float spawnDelay)
	{
		this.name = name;
		this.enemyPrefab = enemyPrefab;
		this.numOfEnemies = numOfEnemies;
		this.spawnDelay = spawnDelay;
		hasSpawned = false;
		hasCleared = false;
		numSpawned = 0;
		numLeft = numOfEnemies;
	}
	#endregion

	#region Methods
	public void StartSpawn()
	{
		if(hasCleared) {
			Debug.LogError("Wave already cleared");
			return;
		}
		else if(hasSpawned) {
			Debug.LogError("Wave already spawned");
			return;
		}

		hasSpawned = true;
	}

	public void EnemySpawned()
	{
		if(!hasSpawned) {
			Debug.LogError("Wave not spawned yet");
			return;
		}
		else if(hasCleared) {
			Debug.LogError("Wave already cleared");
			return;
		}

		numSpawned++;
	}

	public void EnemyRemoved()
	{
		numLeft--;

		if(numLeft == 0)
			hasCleared = true;
	}
	#endregion
}
