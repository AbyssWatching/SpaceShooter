using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private int PowerUpID;
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
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (PowerUpID)
                {
                    case 0:
                        player.ActivateTrippleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2: 
                        player.ActivateShieldPowerUp();
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
