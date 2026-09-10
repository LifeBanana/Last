using UnityEngine;
using UnityEngine.SceneManagement;
//the enemy health and where they take damage or be destroyed
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
    //damages the enemy health and shows damage pop up 
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
    //increase score before destruction
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