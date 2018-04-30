using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public PlayerController g_playerHealth;       // Reference to the player's heatlh.
	public GameObject g_enemy;                // The enemy prefab to be spawned.
	public GameObject g_bigEnemy;
	public GameObject g_fastEnemy;
	public float g_timeBetweenSpawn = 1f;            // How long between each spawn.
	public float g_timeBeforWave = 10f;
	public int g_spawnCount;
	public Transform[] g_spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private bool g_isStartSpawning;
	private int g_waveNumber;

	private bool g_isBossSpawned;

	private bool g_canFastEnemySpawn;
	private int g_fastEnemyNumber;


	void Start ()
	{
		g_isStartSpawning = false;
		g_waveNumber = 0;
		g_isBossSpawned = false;
	}



	void Update(){
		if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && !g_isStartSpawning){
			StartCoroutine (SpawnWaves ());
			g_isStartSpawning = true;
			g_waveNumber++;
			Debug.Log ("Wave n°"+g_waveNumber);
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

		g_isBossSpawned = false;

		if (g_waveNumber > 2) {
			g_canFastEnemySpawn = true;
		}
		g_fastEnemyNumber = g_spawnCount / 5;

		for (int i = 0; i < g_spawnCount; i++)
		{
			int spawnPointIndex = Random.Range (0, g_spawnPoints.Length);
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			GameObject newEnemy = null;
			if (g_waveNumber % 5 == 0 && !g_isBossSpawned) {
				newEnemy = Instantiate (g_bigEnemy, g_spawnPoints [spawnPointIndex].position, g_spawnPoints [spawnPointIndex].rotation);
				g_isBossSpawned = true;
			} else {
				if (g_canFastEnemySpawn && i>=g_spawnCount-g_fastEnemyNumber) {
					newEnemy = Instantiate (g_fastEnemy, g_spawnPoints [spawnPointIndex].position, g_spawnPoints [spawnPointIndex].rotation);
				}
				newEnemy = Instantiate (g_enemy, g_spawnPoints [spawnPointIndex].position, g_spawnPoints [spawnPointIndex].rotation);
			}

			EnemyController enemyStats = newEnemy.GetComponent<EnemyController> ();
			enemyStats.g_maxHealth = enemyStats.g_maxHealth * (0.5f+g_waveNumber*0.2f);
			enemyStats.g_attackDamage = enemyStats.g_attackDamage * (0.5f+g_waveNumber*0.2f);

			if (i == (g_spawnCount-1)) {
				// reset wave
				g_isStartSpawning = false;
			}
			yield return new WaitForSeconds (g_timeBetweenSpawn);
		}
	}

}
