using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] GameObject _explostion;
    [SerializeField] int _speed = 5;
    
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Vector3 RoatationDirection = new Vector3(0,0,1);
        transform.Rotate(RoatationDirection * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Laser"))
        {
            Instantiate(_explostion,this.transform.position,Quaternion.identity);
            Destroy(collider.gameObject);
            Destroy(this.gameObject, .03f);
        }

    }

}
