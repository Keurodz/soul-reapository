using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject spellProjectile;
    public GameObject projectileSpawn;
    public float shootRate = 2f;
    public GameObject deadVFX;

    GameObject[] wanderpoints;
    Vector3 nextDestination;
    // Animator anim;
    float distanceToPlayer;
    float elapsedTime = 0;
    bool isDead;

    EnemyHealth enemyHealth;
    int health;

    Transform deadTransform;

    int currentDestinationIndex = 0;

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
        isDead = false;
        wanderpoints = GameObject.FindGameObjectsWithTag("EnemyWanderpoint");
        // anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        // projectileSpawn = GameObject.FindGameObjectWithTag("Pupil");

        enemyHealth = GetComponent<EnemyHealth>();
        health = enemyHealth.currentHealth;

        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        print("Patrolling");

        // anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    }
    void UpdateChaseState()
    {
        print("Chasing");

        nextDestination = player.transform.position;

        // anim.SetInteger("animState", 2);

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
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

        Destroy(gameObject, 3);
    }

    void FindNextPoint()
    {
        nextDestination = wanderpoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderpoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionTarget = (target - transform.position).normalized;
        directionTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

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
        Instantiate(spellProjectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
    }

    private void OnDestroy()
    {
        //Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

}