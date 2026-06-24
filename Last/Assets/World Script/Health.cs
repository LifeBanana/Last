using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxShield = 50;

    float health;
    float shield;

    Respawn respawn;

    void Start()
    {
        health = maxHealth;
        shield = maxShield;

        respawn = GetComponent<Respawn>();
    }

    public void TakeDamage(float damage)
    {
        if (shield > 0)
        {
            float absorbed =  Mathf.Min(shield, damage);

            shield -= absorbed;
            damage -= absorbed;
        }

        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        respawn.respawn();
    }
}
