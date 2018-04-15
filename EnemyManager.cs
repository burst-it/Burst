using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public PlayerController g_playerHealth;       // Reference to the player's heatlh.
	public GameObject g_enemy;                // The enemy prefab to be spawned.
	public float g_timeBetweenSpawn = 1f;            // How long between each spawn.
	public float g_timeBeforWave = 10f;
	public int g_spawnCount;
	public Transform[] g_spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private bool g_isStartSpawning;



	void Start ()
	{
		g_isStartSpawning = false;
	}



	void Update(){
		if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !g_isStartSpawning){
			StartCoroutine (SpawnWaves ());
			g_isStartSpawning = true;
		}
	}



	/*************************************************************************************************************/
	/*												FONCTIONS		  											 */
	/*************************************************************************************************************/


	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (g_timeBeforWave);
		if(g_playerHealth.g_health <= 0f)
		{
			yield return null;
		}

		for (int i = 0; i < g_spawnCount; i++)
		{
			int spawnPointIndex = Random.Range (0, g_spawnPoints.Length);
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			Instantiate (g_enemy, g_spawnPoints[spawnPointIndex].position, g_spawnPoints[spawnPointIndex].rotation);
			if (i == (g_spawnCount-1)) {
				// reset wave
				g_isStartSpawning = false;
			}
			yield return new WaitForSeconds (g_timeBetweenSpawn);
		}
	}

}
