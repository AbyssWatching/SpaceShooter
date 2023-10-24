using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserMovement : MonoBehaviour
{

    [SerializeField] private int _speed = 5;
    [SerializeField] private int topOfTHeScreen = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //it's a laser it's going straight up
        transform.Translate(transform.up * _speed * Time.deltaTime);

        DestroyLaser();

    }

    //destroys laser if it passes the specified y location
	private void DestroyLaser()
	{
        if (transform.position.y >= topOfTHeScreen)
        {
            Destroy(this.gameObject);
        }
    }
}


