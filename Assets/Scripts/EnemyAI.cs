using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Tracking,
        Attacking
    }

    [SerializeField] private float detectionRadius = 10.0f; // Detection radius for the player
    [SerializeField] private float attackRange = 2.0f; // Distance to start attacking
    [SerializeField] private float idleWanderRadius = 5.0f; // Radius for wandering
    [SerializeField] private float idleWanderDelay = 3.0f; // Time between wandering movements

    private State currentState = State.Idle;
    private Transform player;
    private NavMeshAgent navMeshAgent;

    private float idleTimer = 0f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;

            case State.Tracking:
                HandleTrackingState();
                break;

            case State.Attacking:
                HandleAttackingState();
                break;
        }

        CheckForPlayer();
    }

    private void HandleIdleState()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleWanderDelay)
        {
            // Pick a random point within the idle wander radius
            Vector3 randomDirection = Random.insideUnitSphere * idleWanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, idleWanderRadius, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position);
            }

            idleTimer = 0f; // Reset the timer
        }

        navMeshAgent.isStopped = false; // Ensure the agent is moving
    }

    private void HandleTrackingState()
    {
        if (player == null) return;

        // Move towards the player
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            ChangeState(State.Attacking);
        }
    }

    private void HandleAttackingState()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            ChangeState(State.Tracking);
        }
        else
        {
            // Perform attack action (currently prints attack)
            Debug.Log("Attack!");
        }
    }

    private void CheckForPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (currentState == State.Idle && distanceToPlayer <= detectionRadius)
        {
            ChangeState(State.Tracking);
        }
        else if (currentState == State.Tracking && distanceToPlayer > detectionRadius)
        {
            ChangeState(State.Idle);
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;

        if (newState == State.Idle)
        {
            idleTimer = 0f; // Reset idle timer on entering Idle state
        }
    }
}
