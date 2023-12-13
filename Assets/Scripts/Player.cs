using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
    
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private GameObject _tripleShotPowerUp;
    [SerializeField] private GameObject _shieldVisual;
    [SerializeField] private GameObject _onFirstHit;
    [SerializeField] private GameObject _onSecondeHit;
    [SerializeField] private float _firerate = 0.5f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _activeTrippleShot = false;
    [SerializeField] private bool _activeSpeedBoost = false;
    [SerializeField] private bool _activeShieldPowerUp = false;
    [SerializeField] private int _timerForPowerUps = 5;
    [SerializeField] private int _score = 0;
    [SerializeField] private float _speed = 8;
    [SerializeField] private AudioClip _laserSound;
    private UIManager _manager;
    private Spawnmanager _spawnmanager;
    private AudioSource _audioSource;
    private float _canFire = -1f;
    private float _negativeXLimit = -9f;
    private float _justInNegativeLimit = -8.9f;
    private float _positiveXLimit = 9f;
    private float _justInPositiveLimit = 8.9f;
    private float _negativeYLimit = -3;
    private float _positveYLimit = 1;
    private float _defaultSpeed = 8;
    private float _speedBoostSpeed = 16;

    public delegate void OnPlayerDeath();
    public static OnPlayerDeath onPlayerDeath;
    // these are the clasesses listening 
    //SpawnManager, Player, UiManager

    void Start()
    {
        //null check and finding scripts
        _spawnmanager = GameObject.Find("SpawnManager").GetComponent<Spawnmanager>();

        if (_spawnmanager == null)
        {
            Debug.LogError("Spawnmanager is null in Player Script, Thx!");
        }

        
        _manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
        if (_manager == null)
        {
            Debug.LogError("Canvas couldn't be found");
        }
        
        
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("Ain't no audio source on the player, playa");
        }else
        {
            _audioSource.clip = _laserSound;
        }

        onPlayerDeath += PlayerDeath;



        //making default position of main character
        this.transform.position = new Vector3(0f,-3.14f,0f);

        


    }

    void Update()
    {
        Movement();
        FireRate();
    
    
    }

     private void Movement()
     {

        float horizontalInput = Input.GetAxisRaw(HORIZONTAL);

        float verticalInput = Input.GetAxisRaw(VERTICAL);

        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);

        direction.Normalize();

        transform.Translate(direction * Time.deltaTime * _speed);

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
    //how often one can fire
    private void FireRate()
    {
         if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
      {
            FireLaser();
      }
    }
     //laser fire to include powerup
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
        _audioSource.Play();
		_canFire = _firerate + Time.time;
		
    }

    //method for if damage is taken by player
    [ContextMenu ("Damage")]
    public void Damage()
    {
        if (_activeShieldPowerUp == true)
        {
            Scored(10);
            //shield power up is up so no dying for now hahaha!
        }else
        {
            _lives--;

            _manager.updateLives(_lives);

            if (_lives <= 2)
            {
                _onFirstHit.SetActive(true);
            }
            if (_lives <= 1)
            {
                _onSecondeHit.SetActive(true);
            }
            if (_lives <= 0 && onPlayerDeath != null)
            {
                onPlayerDeath?.Invoke();
                _spawnmanager.OnPlayerDeath();
                

            }
            Scored(5);
        }
      
     } 

     public void PlayerDeath()
     {
        Destroy(gameObject);
     }
    //testing purposes
     public void SpeedBoostSecret()
     {
        if (_activeSpeedBoost == true)
        {
            ActivateSpeedBoost();
        } 
     }
    //speed boost power up activation
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
    //shield power up activation
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
    //scoring logic, is passed from enemies using this method
     public void Scored(int points)
     {
        _score += points;

        _manager.updateScore(_score);
     }
}


