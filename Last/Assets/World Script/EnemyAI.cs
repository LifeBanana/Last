using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public NavMeshAgent agent;

    public float detectionRange = 20f;

    public float attackRange = 8f;

    public float damage = 10f;

    public float attackRate = 1f;

    float nextAttack;

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance( transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            Attack();
        }
        else if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        if (Time.time < nextAttack)
            return;

        nextAttack = Time.time + attackRate;

        Health health = player.GetComponent<Health>();

        if (health != null)
            health.TakeDamage(damage);
    }
}
