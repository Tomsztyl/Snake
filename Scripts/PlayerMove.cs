using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    public float rotationX, rotationY, rotationZ;
    [SerializeField]
    private UIManager _UImanager;
    [SerializeField]
    private GameObject bodySnake;
    [SerializeField]
    public List<Transform> bodyParts = new List<Transform>();
    [SerializeField]
    private GameObject player_Head;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private float speedPowerUp = 15f;
    [SerializeField]
    private float defaultSpeed = 5f;
    //[SerializeField]
    //private AudioSource _audiosource;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _UImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        //InputButton();
        //PowerUpSpeedBoost();
        MovementPlayer();
    }
    public void PowerUpSpeedBoost()
    {
        speed=speedPowerUp;
        StartCoroutine(PowerUpSpeedBoostDelay());
    }
    IEnumerator PowerUpSpeedBoostDelay()
    {
        yield return new WaitForSeconds(5.0f);
        speed = defaultSpeed;
    }
    private void CheckRotation()
    {
        //CheckRotationPlayer
        rotationX = transform.eulerAngles.x;
        rotationY = transform.eulerAngles.y;
        rotationZ = transform.eulerAngles.z;
    }
    public void InputButtonUp()
    {
            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
    public void InputButtonLeft()
    {
            transform.eulerAngles = new Vector3(rotationX, rotationY, 90);
    }
    public void InputButtonDown()
    {
            transform.eulerAngles = new Vector3(rotationX, rotationY, 180);
    }
    public void InputButtonRight()
    {
            transform.eulerAngles = new Vector3(rotationX, rotationY, 270);
    }
    private void MovementPlayer()
    {
        if (rotationZ == 0)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (rotationZ == 90)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (rotationZ == 180)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else if (rotationZ == 270)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Walls")
        {
            _spawnManager.DelSnakeBody();
            player_Head.active = false;
        }
        if (collision.tag=="Points")
        {
            //_audiosource.Play(0);
            Destroy(collision.gameObject);
            _UImanager.UpdateScore();
        }
    }
}
