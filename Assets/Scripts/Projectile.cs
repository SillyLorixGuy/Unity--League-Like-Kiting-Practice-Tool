using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float damage;
    public float maxTravelDistance = 10f;
    public Vector3 direction;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;

        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DummyHealth health = other.GetComponent<DummyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
