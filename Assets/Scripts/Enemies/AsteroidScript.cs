using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] GameObject _explostion;
    [SerializeField] int _speed = 5;
    private Spawnmanager _spawnManage;
    
    void  Start() 
    {
        _spawnManage = GameObject.Find("SpawnManager").GetComponent<Spawnmanager>();
        if (_spawnManage == null)
        {
            Debug.Log("Hey Dummy spawnManager wasn't found");
        }    
    }
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
            _spawnManage.StartRound();
            Destroy(collider.gameObject);
            Destroy(this.gameObject, .03f);
        }

    }

}
