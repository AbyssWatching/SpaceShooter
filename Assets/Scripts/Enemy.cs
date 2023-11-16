using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int _speed = 5;
    private Player _player;
    private int bottomOfScreen = -7;
    private float topOfScreen = 8.0f;
    private float maxMinimalXrangeforSPawn = -10.0f;
    private float maxMaximalXrangeForSpawn  = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

         if (_player == null)
        {
            Debug.LogError("_Player is null in Enemy Script, Thx!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        ResetEnemyPosition();
        

        Movement();
    }


    void Movement() 
    {
		transform.position += Vector3.down * Time.deltaTime * _speed;
	}

    void ResetEnemyPosition() 
    {
		if (transform.position.y <= bottomOfScreen)
		{
			RespawnEnemy();
		}
	}

    public void RespawnEnemy() 
    {


        transform.position = new Vector3((UnityEngine.Random.Range(maxMinimalXrangeforSPawn, maxMaximalXrangeForSpawn)), topOfScreen, 0.0f);


    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log(other.tag + " was hit");

        if (other.CompareTag("Laser"))
        {
           if (_player != null)
           {
            _player.Scored(10);
           }

            Destroy(other.gameObject);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            Debug.Log("able to ge component :" + other.name);

            if (player != null)
            {
                player.Damage();
                Debug.Log("was able to damage");
            }
            else
            {Debug.LogError("Enemy script has a null player ref check ontriggerEnter method first.");}

            Destroy(gameObject);

        }
    }
}
