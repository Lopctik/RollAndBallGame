using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float force = 16;
    public GameObject focalPoint;
    public bool hasPowerUp = false;
    public float powerUpStrength = 30;
    public GameObject powerUpRing;
    private Rigidbody _playerRB;
    private float _verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
    }

    // FixedUpdate - вызывается 50 раз в сек. и нужен для оброботки физики Unity
    void FixedUpdate()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _playerRB.AddForce(focalPoint.transform.forward * _verticalInput * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GarbageCollector"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("MassBooster"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            _playerRB.transform.localScale = new Vector3(2, 2, 2);
            _playerRB.mass = 8;
            force = 20;
            powerUpRing.SetActive(true);
            StartCoroutine(PowerUpCourutine());
        }
        if(other.gameObject.CompareTag("SpeedBooster"))
        {
            hasPowerUp = true;
            force = 25;
            Destroy(other.gameObject);
            powerUpRing.SetActive(true);
            StartCoroutine(PowerUpCourutine());
        }
        if(other.gameObject.CompareTag("SlowBooster"))
        {
            hasPowerUp = true;
            force = 5;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCourutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _playerRB.AddForce(focalPoint.transform.forward * 3, ForceMode.Impulse);
        }
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Debug.Log("Player collided with " + collision.gameObject + " with powerUp");
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerUpCourutine()
    {
        yield return new WaitForSeconds(15);
        hasPowerUp = false;
        _playerRB.transform.localScale = new Vector3(1, 1, 1);
        _playerRB.mass = 1;
        force = 16;
        powerUpRing.SetActive(false);
        Debug.Log("Буст закончился");
    }
}
