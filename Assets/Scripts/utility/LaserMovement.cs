using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserMovement : MonoBehaviour
{

    [SerializeField] private int _speed = 5;
    [SerializeField] private int _topOfTHeScreen = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
       void Update()
    {
        //it's a laser it's going straight up
       
        Movement();
        DestroyLaser();

    }

    //destroys laser if it passes the specified y location
	private void DestroyLaser()
	{
        if (transform.position.y >= _topOfTHeScreen)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    //it's a laser it's going straight up
    private void Movement()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime);
    }
}


