using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Animator _animator;
    private Player _player;
    private AudioSource _audioSource;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        //assign the component to Anim
        if(_player == null)
        {
            Debug.LogError("the Player is NULL!");
        }

        _animator = GetComponent<Animator>();

        if(_animator == null)
        {
            Debug.LogError("Animator is null");
        }
    }

    void Update()
    {
        //move down at 4 meters per second
        // if button on screen ->respawn on top with a new random x position

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5f)
        {
            var randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is Player -> destroy us -> damage player
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            //trigger anim
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.2f);
        }

        // if other is laser -> destroy laser -> destroy us 
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(1);
            }
            //trigger anim before destroy
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>()); //for destroying enemy and oportunity for have sound (shoot twise)

            Destroy(this.gameObject, 2.2f);
        }         
    }
}
