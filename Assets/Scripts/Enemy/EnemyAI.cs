using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target { private get; set; }

    [SerializeField] Transform bulletPoint;
    [SerializeField] AudioSource bulletSource;
    [SerializeField] Animator enemy;

    [SerializeField] GameObject enemyBulletPrefab;
    Pool<GameObject> enemyPoolBullet;
    int maxEnemyBullet;
    private List<GameObject> enemyBulletObjects = new List<GameObject>();

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public Vector3 walkPoint;
    public float walkPointRange;
    public int bulletSpeed = 1;

    public float timeBetweenAttacks;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private int currentWaypointIndex = 0;
    Vector3 Next_Point;
    public List<Transform> Waypoints = new List<Transform>();
    private float cooldownTimer;
    private float shootCooldown = 1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target");
    }

    void Start()
    {
        maxEnemyBullet = 3;
        var index = Random.Range(0, 11);
        enemyPoolBullet = new Pool<GameObject>(CreateBullet, (gameObject) => gameObject.SetActive(true), (gameObject) => gameObject.SetActive(false), maxEnemyBullet);
        agent.SetDestination(Next_Point);
        agent.transform.rotation = Quaternion.identity;

        if (Waypoints.Count > 0)
        {
            agent.SetDestination(Waypoints[currentWaypointIndex].position);
        }
    }

    private void OnEnable()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Attack();
        }
        else if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
    }

    private GameObject CreateBullet()
    {
        var bullet = Instantiate(enemyBulletPrefab);
        bullet.GetComponent<HotDogBullet>().eai = this;

        return bullet;
    }

    public void ResetPool()
    {
        enemyBulletObjects.ForEach((gameObject) => ReturnBullet(gameObject));
    }

    public void ReturnBullet(GameObject bullet)
    {
        enemyPoolBullet.ReturnObject(bullet);
        enemyBulletObjects.Remove(bullet);
    }

    void InstantiateBullet()
    {
        Vector3 directionToPlayer = (target.transform.position - transform.position);
        GameObject enemyBulletInstance = enemyPoolBullet.GetObject();
        enemyBulletInstance.transform.position = bulletPoint.position;
        enemyBulletInstance.transform.forward = directionToPlayer; 
        enemyBulletObjects.Add(enemyBulletInstance);
    }

    public virtual void Shoot()
    {
        if (cooldownTimer <= 0f)
        {
            bulletSource.Play();
            InstantiateBullet();
            cooldownTimer = shootCooldown;
        }
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target.transform);
        enemy.SetBool("OnAttack", true);
        enemy.SetBool("OnPatrol", false);
        enemy.SetBool("OnChase", false);
    }

    void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            enemy.SetBool("OnPatrol", true);
            enemy.SetBool("OnChase", false);
            enemy.SetBool("OnAttack", false);
            currentWaypointIndex = UnityEngine.Random.Range(0, Waypoints.Count);
            agent.SetDestination(Waypoints[currentWaypointIndex].position);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            Chase();
        }
    }

    void Chase()
    {
        enemy.SetBool("OnChase", true);
        enemy.SetBool("OnPatrol", false);
        enemy.SetBool("OnAttack", false);
        agent.enabled = true;
        agent.speed = 5f;
        agent.SetDestination(target.transform.position);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            playerInSightRange = false;
            playerInAttackRange = false;
            Patrolling();
        }
    }
}
