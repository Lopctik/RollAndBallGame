using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 5;
    private Rigidbody _rbEnemy;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _rbEnemy.AddForce(direction * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GarbageCollector"))
        {
            Destroy(gameObject);
        }
    }
}
