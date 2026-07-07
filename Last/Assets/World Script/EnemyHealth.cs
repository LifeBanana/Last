using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(name + " took " + damage);

        currentHealth -= damage;

        Popupmanager.Instance.ShowPopup(transform.position + Vector3.up, damage);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
