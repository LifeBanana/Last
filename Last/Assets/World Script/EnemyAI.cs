using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum AIState
    {
        Patrol,
        Chase,
        Attack
    }

    [Header("Target")]
    public Transform player;

    [Header("AI State")]
    public AIState currentState = AIState.Patrol;

    [Header("Detection")]
    public float detectionRange = 20f;
    public float attackRange = 8f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    [Header("Flying")]
    public bool canFly = false;
    public float flyHeight = 5f;
    public float verticalSpeed = 2f;

    [Header("Patrolling")]
    public Transform[] patrolPoints;
    public float patrolWaitTime = 2f;

    private int currentPatrolPoint = 0;
    private float patrolWaitTimer;

    [Header("Combat")]
    public float damage = 10f;
    public float attackRate = 1f;

    private float nextAttack;

    private void Awake()
    {
        FindPlayer();
    }

    private void Start()
    {
        FindPlayer();

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentState = AIState.Patrol;
        }
    }

    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector3.Distance(  transform.position, player.position );

        if (distance <= attackRange)
        {
            currentState = AIState.Attack;
        }
        else if (distance <= detectionRange)
        {
            currentState = AIState.Chase;
        }
        else
        {
            currentState = AIState.Patrol;
        }

        switch (currentState)
        {
            case AIState.Patrol:
                Patrol();
                break;

            case AIState.Chase:
                Chase();
                break;

            case AIState.Attack:
                AttackState();
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints == null ||
            patrolPoints.Length == 0)
        {
            return;
        }

        Transform target = patrolPoints[currentPatrolPoint];

        if (target == null)
            return;

        Vector3 targetPosition =  target.position;

        if (canFly)
        {
            targetPosition.y = target.position.y + flyHeight;
        }

        Vector3 direction = targetPosition - transform.position;

        float distance = direction.magnitude;

        if (distance <= 0.5f)
        {
            patrolWaitTimer += Time.deltaTime;

            if (patrolWaitTimer >= patrolWaitTime)
            {
                patrolWaitTimer = 0f;

                currentPatrolPoint++;

                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            }

            return;
        }

        patrolWaitTimer = 0f;

        MoveTowards(targetPosition);
    }

    private void Chase()
    {
        Vector3 targetPosition =  player.position;

        if (canFly)
        {
            targetPosition.y = player.position.y;
        }

        MoveTowards(targetPosition);

        LookAtTarget(player.position);
    }

    private void AttackState()
    {

        LookAtTarget(player.position);

        Attack();
    }

    private void MoveTowards( Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        if (direction.sqrMagnitude <= 0.01f)
            return;

        direction.Normalize();

        transform.position +=  direction *  moveSpeed * Time.deltaTime;

        LookAtTarget(targetPosition);
    }

    private void LookAtTarget(  Vector3 targetPosition)
    {
        Vector3 direction =  targetPosition - transform.position;

        if (direction.sqrMagnitude <= 0.01f)
            return;

        Quaternion targetRotation =  Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(   transform.rotation,  targetRotation,  rotationSpeed *   Time.deltaTime );
    }

    private void Attack()
    {
        if (Time.time < nextAttack)
            return;

        nextAttack = Time.time + attackRate;

        Health health =  player.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }


    private void FindPlayer()
    {
        Controller controller = FindFirstObjectByType<Controller>();

        if (controller != null)
        {
            player = controller.transform;

            Debug.Log( "Player found using Controller Script " +  player.name );

            return;
        }

        GameObject playerObject =  GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}