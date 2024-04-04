using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;
    public float enemySpeed = 5;
    public float chaseDistance = 10;
    public float attackDistance = 2;
    public GameObject player;
    public GameObject[] spellProjectiles;
    public GameObject enemyEye;
    public float shootRate = 2f;
    public GameObject deadVFX;
    public float fov = 45f;

    GameObject[] wanderpoints;
    Vector3 nextDestination;
    // Animator anim;
    float distanceToPlayer;
    float elapsedTime = 0;
    bool isDead;
    Transform deadTransform;

    EnemyHealth enemyHealth;
    int health;

    int currentDestinationIndex = 0;

    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        health = enemyHealth.currentHealth;

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;

        }

        elapsedTime += Time.deltaTime;

        if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }
    void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        isDead = false;
        wanderpoints = GameObject.FindGameObjectsWithTag("EnemyWanderpoint");
        // anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;

        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        print("Patrolling");

        // anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;

        agent.speed = enemySpeed - (enemySpeed * 0.2f);

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (CanSeePlayer())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    void UpdateChaseState()
    {
        print("Chasing");

        nextDestination = player.transform.position;

        agent.stoppingDistance = attackDistance;

        agent.speed = enemySpeed;

        // anim.SetInteger("animState", 2);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        // anim.SetInteger("animState", 3);

        EnemySpellCast();
    }
    void UpdateDeadState()
    {
        // anim.SetInteger("animState", 4);
        deadTransform = gameObject.transform;
        isDead = true;
        print("Angel is dead");

        Destroy(gameObject);
    }

    void FindNextPoint()
    {
        nextDestination = wanderpoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderpoints.Length;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionTarget = (target - transform.position).normalized;
        directionTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void EnemySpellCast()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {
                // var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("SpellCasting", 3f);
                elapsedTime = 0.0f;

            }
        }
    }

    void SpellCasting()
    {
        int randProjectileIndex = Random.Range(0, spellProjectiles.Length);

        GameObject spellProjectile = spellProjectiles[randProjectileIndex];

        Instantiate(spellProjectile, enemyEye.transform.position, enemyEye.transform.rotation);
    }

    private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;

        Vector3 directionToPlayer = player.transform.position - enemyEye.transform.position;

        if (Vector3.Angle(directionToPlayer, enemyEye.transform.forward) <= fov)
        {
            if (Physics.Raycast(enemyEye.transform.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("Player in view");
                    return true;
                }

                return false;
            }

            return false;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEye.transform.position + (enemyEye.transform.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fov * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fov * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEye.transform.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEye.transform.position, rightRayPoint, Color.magenta);
        Debug.DrawLine(enemyEye.transform.position, leftRayPoint, Color.yellow);

    }

}