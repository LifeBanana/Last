using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    private float damage;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position +=   transform.forward *  speed *   Time.deltaTime;

        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (collision.gameObject.CompareTag("Dummy"))
        {
            Dummy dummy = collision.gameObject.GetComponent<Dummy>();

            if (dummy != null)
            {
                dummy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.gameObject.name);

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Dummy dummy = other.GetComponent<Dummy>();

        if (dummy != null)
        {
            dummy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
