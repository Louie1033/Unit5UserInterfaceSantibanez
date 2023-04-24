using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;

    public int pointValue;

    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }


    private void OnMouseDown()
    {
        if(gameManager.isGameActive && gameManager.gamePaused == false)
        {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
        }
        if(gameManager.isGameActive && gameObject.CompareTag("Bad") && gameManager.gamePaused == false)
        {
            gameManager.UpdateLives(-1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!gameObject.CompareTag("Bad") && gameManager.lives >= 1)
        {
            gameManager.UpdateLives(-1);
            //gameManager.GameOver();
        }
        Destroy(gameObject);
    }
    private void GameOver()
    {
        if(gameManager.lives < 1)
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}

