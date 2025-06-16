using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed = 7f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;

    public int _lives = 3;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;

    //variable isTripleShotActive 
    [SerializeField]
    private bool _isTripleShotActive = false;
    //variable for increase speed (_speedMultiplier)
    [SerializeField]
    private bool _isSpeedBoostActive;
    [SerializeField]
    private bool _isShieldsActive = false;

    //variable reference to the shield visualaser
    [SerializeField]
    private GameObject _shieldsVisualaser;

    [SerializeField]
    private int _score ;

    private UIManager _uimanager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
        }

        if (_uimanager == null)
        {
            Debug.LogError("The UI manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AUDIO SOURCE on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 diraction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(diraction * _speed *  Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, 0, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, 0, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            // or instantiate 3 lasers (triple shot prefab)
            Instantiate(_TripleShotPrefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
        }

        // play the lase audio clip
        _audioSource.Play();

    }

    public void Damage()
    {
        // if shields is active -> do nothing -> deactivated shields -> return;
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            // shield visualaser
            _shieldsVisualaser.SetActive(false);
            return;

        }

        _lives--;

        _uimanager.UpdateLives(_lives);
        //check if dead -> destroy us
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject); //player
        }
    }

    public void TripleShotActive()
    {
        //tsa become true 
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;

        //enable the visualaser 
        _shieldsVisualaser.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uimanager.UpdateScore(_score);
    }
}
