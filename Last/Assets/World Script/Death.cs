using UnityEngine;

public class Death : MonoBehaviour
{
    [Header("Y Boundary")]
    public bool useYBoundary = true;

    public float deathY = -50f;

    [Header("Trigger")]
    public bool useTrigger = true;

    private void Update()
    {
        if (!useYBoundary)
            return;

        if (!CompareTag("Player"))
            return;

        if (transform.position.y <= deathY)
        {
            KillPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger)
            return;

        if (!other.CompareTag("Player"))
            return;

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.DieFromDeathBarrier();
        }
    }

    private void KillPlayer()
    {
        Health health = GetComponent<Health>();

        if (health != null)
        {
            health.DieFromDeathBarrier();
        }
    }
}