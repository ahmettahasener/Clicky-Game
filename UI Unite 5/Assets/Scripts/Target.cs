using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    public int live = 3;
    public float minSpeed = 16;
    public float maxSpeed = 20;
    public float maxTorque = 10;
    public float xRange = 4;
    public float ySpawnPos = -2;
    public int pointValue;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(),ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive) 
        { 
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.ScoreUpdate(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (gameObject.CompareTag("Sensor") && other.gameObject.CompareTag("Good"))
        {
            live -= 1;
            //gameManager.GameOver();
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
        if (gameObject.CompareTag("Sensor"))
        {
            return new Vector3(0,-10,0);
        }
        else
        {
            return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
        }
        
    }
}
