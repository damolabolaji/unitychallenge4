using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private float powerUpForce = 15;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject PowerUpIndicator;
    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed* forwardInput);

        PowerUpIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            PowerUpIndicator.SetActive(true);   
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCtDnRoutine());
        }
    }

    IEnumerator PowerUpCtDnRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        PowerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);

            Debug.Log("Collided with" + collision.gameObject.name + "with power up set to" + hasPowerUp);
        }
    }

}
