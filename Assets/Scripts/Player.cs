using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _firerate = 0.5f;
    private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _tripleShotPowerUp;
    [SerializeField] private GameObject _shieldVisual;
    [SerializeField] private bool _activeTrippleShot = false;
    [SerializeField] private bool _activeSpeedBoost = false;
    [SerializeField] private bool _activeShieldPowerUp = false;
    [SerializeField] private int _timerForPowerUps = 5;
    private Spawnmanager _spawnmanager;
    private float _negativeXLimit = -9f;
    private float _justInNegativeLimit = -8.9f;
    private float _positiveXLimit = 9f;
    private float _justInPositiveLimit = 8.9f;
    private float _negativeYLimit = -3;
    private float _positveYLimit = 1;
    [SerializeField] private float _speed = 8;
    private float _defaultSpeed = 8;
    private float _speedBoostSpeed = 16;



    void Start()
    {

        _spawnmanager = GameObject.Find("SpawnManager").GetComponent<Spawnmanager>();

        if (_spawnmanager == null)
        {
            Debug.LogError("Spawnmanager is null in Player Script, Thx!");
        }
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

     private void Movement()
     {

        float horizontalInput = Input.GetAxisRaw(HORIZONTAL);

        float verticalInput = Input.GetAxisRaw(VERTICAL);

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        direction.Normalize();

        transform.Translate(direction * Time.deltaTime * _speed);

        //if this.position >= 9    then this.position == -8.9

        if(this.transform.position.x <= _negativeXLimit)
        {
            transform.position = new Vector3(_justInPositiveLimit,transform.position.y,0f);
        }
        if (this.transform.position.x >= _positiveXLimit)
        {
            transform.position = new Vector3(_justInNegativeLimit,transform.position.y,0f);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,_negativeYLimit, _positveYLimit),0);

} 
     
    void FireLaser() 
    { 
        if (_activeTrippleShot == false)
        {
            Instantiate(_laserPrefab, (this.transform.position + new Vector3(0, 1.1f, 0)), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPowerUp, (this.transform.position + new Vector3(-.4f, .9f, 0)), Quaternion.identity);
        }

		_canFire = _firerate + Time.time;
		
    }

    [ContextMenu ("Damage")]
    public void Damage()
    {
        if (_activeShieldPowerUp == true)
        {
            //shield power up is up so no dying for now hahaha!
        }else
        {
            _lives--;

            if (_lives <= 0)
            {
                Destroy(gameObject); 
                _spawnmanager.OnPlayerDeath();
            }
        }
      
     } 

     public void SpeedBoostSecret()
     {
        if (_activeSpeedBoost == true)
        {
            ActivateSpeedBoost();
        } 
     }

     [ContextMenu ("speedBoost")]
     public void ActivateSpeedBoost()
     {
        _speed = _speedBoostSpeed;

        StartCoroutine(DeactivateSpeedBoost());
     }

     public IEnumerator DeactivateSpeedBoost() 
    {
        yield return new WaitForSeconds(_timerForPowerUps);
        _speed = _defaultSpeed;
        _activeSpeedBoost = false;
    }

    public void ActivateShieldPowerUp()
    {
        _activeShieldPowerUp = true;

        _shieldVisual.SetActive(true);

        StartCoroutine(DeactivateShields());

    }

    private IEnumerator DeactivateShields()
    {
        yield return new WaitForSeconds(_timerForPowerUps);
        _activeShieldPowerUp = false;
        _shieldVisual.SetActive(false);
    }

     public void ActivateTrippleShot()
     {
        _activeTrippleShot = true;

        StartCoroutine(DeactivateTrippleShot());
     }

     private IEnumerator DeactivateTrippleShot()
     {
        yield return new WaitForSeconds(_timerForPowerUps);
        _activeTrippleShot = false;
     }
}


