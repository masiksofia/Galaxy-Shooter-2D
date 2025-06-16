using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    //ID for powerups -> 0 = triple shot -> 1 = speed -> 2 = shields
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _audioClip;

   

    void Update()
    {
        // move down at a speed of 3

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            if (player != null)
            {
                //if powerup is 0
                if (powerupID == 0)
                {
                    player.TripleShotActive();
                }
                //else if powerup is 1 -> play speed powerup
                // else if is 2  -> shields powerup
                else if (powerupID == 1)
                {
                    player.SpeedBoostActive();
                }
                else if (powerupID == 2)
                {
                    player.ShieldsActive();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
