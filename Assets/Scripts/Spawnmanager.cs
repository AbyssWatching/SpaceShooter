using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnmanager : MonoBehaviour
{

    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private int _minTime = 3;
    [SerializeField] private int _maxTime = 7;    
	private float _maxMinimalXrangeforSPawn = -10.0f;
	private float _maxMaximalXrangeForSpawn = 10.0f;
    private bool _stopSpawning = false;

	
    void Start()
    {
        //starting the coroutines
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpRoutine());

    }

//spawns enemies while the player has lives
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(_maxMinimalXrangeforSPawn, _maxMaximalXrangeForSpawn), 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_minTime);
        }
    }
    //spawning of powerups
    IEnumerator PowerUpRoutine()
    {
        while(_stopSpawning == false){

        int randomPowerUp = (int)Random.Range(0,3);
       Instantiate(_powerUps[randomPowerUp], new Vector3(Random.Range(_maxMinimalXrangeforSPawn, _maxMaximalXrangeForSpawn), 8, 0), Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(_minTime,_maxTime));
        }
    }
    //prevents spawning of new GO
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
