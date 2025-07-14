using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State currentState;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float attackCooldown = 2f;

    public Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
        if (player == null)
        {
            GameObject found = GameObject.FindWithTag("Player");
            if (found != null)
            {
                player = found.transform;
            }
            else
            {
                Debug.LogError("Khong tim thay GameObject co tag 'Player'");
            }
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distance < chaseDistance) currentState = State.Chase;
                break;

            case State.Chase:
                Chase();
                if (distance < attackDistance) currentState = State.Attack;
                else if (distance > chaseDistance) currentState = State.Patrol;
                break;

            case State.Attack:
                Attack();
                if (distance > attackDistance) currentState = State.Chase;
                break;
        }
    }

    void Patrol()
    {
        agent.destination = patrolPoints[currentPatrolIndex].position;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Chase()
    {
        agent.destination = player.position;
    }

    void Attack()
    {
        if (player == null)
        {
            Debug.LogError("Player is NULL trong Attack()");
            return;
        }

        agent.ResetPath();
        transform.LookAt(player);

        if (Time.time > lastAttackTime + attackCooldown)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(20);
                lastAttackTime = Time.time;
            }
            else
            {
                Debug.LogError("Khong tim thay component PlayerHealth tren Player!");
            }
        }
    }
}
public class EnemyHealth : HealthSystem
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
