using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int _speed = 5;
    private int bottomOfScreen = -7;
    private float topOfScreen = 8.0f;
    private float maxMinimalXrangeforSPawn = -10.0f;
    private float maxMaximalXrangeForSpawn  = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= bottomOfScreen) 
        {
            ResetEnemyPosition();
        }

        Movement();
    }


    void Movement() 
    {
		transform.position += Vector3.down * Time.deltaTime * _speed;
	}

    void ResetEnemyPosition() 
    {


        transform.position = new Vector3((Random.Range(maxMinimalXrangeforSPawn, maxMaximalXrangeForSpawn)), topOfScreen, 0.0f);


    }
}
