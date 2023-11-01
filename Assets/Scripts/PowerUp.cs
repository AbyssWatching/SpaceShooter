using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    private int bottomOfScreen = -7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * _speed;

        endOfTheLine();


    }

    private void endOfTheLine()
    {
        if (transform.position.y <= bottomOfScreen)
		{
			Destroy(this.gameObject);
		}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.ActivateTrippleShot();
            }
            else{
                Debug.LogError("Issue in Powerup script check onTriggerEnter method first player ref null");
            }
            Destroy(this.gameObject);
        }
    }
}