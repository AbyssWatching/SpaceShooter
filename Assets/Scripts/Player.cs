using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private float _firerate = 0.5f;
    [SerializeField] private float _canFire = -1f;
     private float negativeXLimit = -9f;
     private float justInNegativeLimit = -8.9f;
     private float positiveXLimit = 9f;
     private float justInPositiveLimit = 8.9f;
     private float negativeYLimit = -3;
     private float positveYLimit = 1;
     private float _speed = 8f;



    void Start()
    {

        //made default position

        this.transform.position = new Vector3(0f,-3.14f,0f);


    }

    // Update is called once per frame
    void Update()
    {
      Movement();

      if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
      {
            FireLaser();
      }

    }

     void Movement()
     {

        float horizontalInput = Input.GetAxisRaw(HORIZONTAL);

        float verticalInput = Input.GetAxisRaw(VERTICAL);

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        direction.Normalize();

        transform.Translate(direction * Time.deltaTime * _speed);

        //if this.position >= 9    then this.position == -8.9

        if(this.transform.position.x <= negativeXLimit)
        {
            transform.position = new Vector3(justInPositiveLimit,transform.position.y,0f);
        }
        if (this.transform.position.x >= positiveXLimit)
        {
            transform.position = new Vector3(justInNegativeLimit,transform.position.y,0f);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3f,1f),0);

     } 
     
    void FireLaser() 
    { 
    
	    Instantiate(LaserPrefab, (this.transform.position + new Vector3(0, 0.8f, 0)), Quaternion.identity);

		_canFire = _firerate + Time.time;
		
    }
}


