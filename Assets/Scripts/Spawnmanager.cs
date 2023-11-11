using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] GameObject enemyContainer;
    [SerializeField] private int minTime = 3;
    [SerializeField] private int maxTime = 7;
    [SerializeField] private GameObject[] powerUps;
    
	private float maxMinimalXrangeforSPawn = -10.0f;
	private float maxMaximalXrangeForSpawn = 10.0f;

    private bool _stopSpawning = false;
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            
           GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(maxMinimalXrangeforSPawn, maxMaximalXrangeForSpawn), 8, 0), Quaternion.identity);

           newEnemy.transform.parent = enemyContainer.transform;



            yield return new WaitForSeconds(minTime);
        }
    }

    IEnumerator PowerUpRoutine()
    {
        while(_stopSpawning == false){

        int randomPowerUp = (int)Random.Range(0,3);
       Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(maxMinimalXrangeforSPawn, maxMaximalXrangeForSpawn), 8, 0), Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(minTime,maxTime));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
