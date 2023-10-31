using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private int time = 3;
	private float maxMinimalXrangeforSPawn = -10.0f;
	private float maxMaximalXrangeForSpawn = 10.0f;
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            
           GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(maxMinimalXrangeforSPawn, maxMaximalXrangeForSpawn), 8, 0), Quaternion.identity);

           newEnemy.transform.parent = enemyContainer.transform;



            yield return new WaitForSeconds(time);
        }
    }
}
