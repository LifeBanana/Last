using UnityEngine;

public class Health : MonoBehaviour
{
    float health;
    float shield;

    Respawn respawn;

    public float maxHealth;
    public float maxShield;

    void Start()
    {
        Profile p = Statsmanager.Instance.Profile;

        maxHealth = p.health;
        maxShield = p.shields;

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
