using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private int _powerUpID;
    [SerializeField] private AudioClip _powerUpPickUpSound;
    private int _bottomOfScreen = -7;

    // Update is called once per frame
    void Update()
    {
        Movement();

        endOfTheLine();


    }
    //resets position of powerups
    private void endOfTheLine()
    {
        if (transform.position.y <= _bottomOfScreen)
		{
			Destroy(this.gameObject);
		}
    }

    private void Movement()
    {
        transform.position += Vector3.down * Time.deltaTime * _speed;
    }
    //decides which power up to use
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(_powerUpPickUpSound,transform.position);
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.ActivateTrippleShot();
                        Debug.Log("Trip shot tripped");
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        Debug.Log("speed boost tripped");
                        break;
                    case 2: 
                        player.ActivateShieldPowerUp();
                        Debug.Log("Shield tripped");
                        break;
                    default:
                        break;
                }
            }
            else{
                Debug.LogError("Issue in Powerup script check onTriggerEnter method first player ref null");
            }
            Destroy(this.gameObject);
        }
    }
}
