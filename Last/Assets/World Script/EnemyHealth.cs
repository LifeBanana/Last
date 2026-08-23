using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public int scoreReward = 100;
    public int skillPointReward = 1;

    private float currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        Debug.Log(  name + " took " + damage + " damage.");

        currentHealth -= damage;

        if (Popupmanager.Instance != null)
        {
            Popupmanager.Instance.ShowPopup( transform.position + Vector3.up,  damage  );
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (Scoremanager.Instance != null)
        {
            Scoremanager.Instance.AddScore(scoreReward);
        }
        else
        {
            Debug.LogWarning( "ScoreManager.Instance is missing.");
        }

        Destroy(gameObject);
    }
}