using UnityEngine;

public class Dummy : MonoBehaviour
{
    public bool infiniteHealth = true;

    EnemyHealth health;

    void Start()
    {
        health =
            GetComponent<EnemyHealth>();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Dummy Hit!");

        Popupmanager.Instance.ShowPopup(transform.position + Vector3.up, damage);

        if (!infiniteHealth)
        {
            health.TakeDamage(damage);
        }
    }
}
