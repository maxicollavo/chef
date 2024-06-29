using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    [SerializeField] AudioSource bulletSource;

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
    }

    void Start()
    {
        maxEnemyBullet = 3;
        var index = Random.Range(0, 11);
        enemyPoolBullet = new Pool<GameObject>(CreateBullet, (gameObject) => gameObject.SetActive(true), (gameObject) => gameObject.SetActive(false), maxEnemyBullet);
        // Next_Point = Waypoints[index].position;
        agent.SetDestination(Next_Point);
        agent.transform.rotation = Quaternion.identity;

        if (Waypoints.Count > 0)
        {
            agent.SetDestination(Waypoints[currentWaypointIndex].position);
        }
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
        enemyBulletInstance.transform.position = transform.position; //ERROR: este transform.position cambiarlo por el arma cuando el enemigo tenga brazos
        enemyBulletInstance.transform.forward = directionToPlayer; //ERROR: este transform.position cambiarlo por el arma cuando el enemigo tenga brazos
        enemyBulletObjects.Add(enemyBulletInstance);

        // var _rb = enemyBulletInstance.GetComponent<Rigidbody>();
        // _rb.velocity = Vector3.zero;
        // //_rb.AddForce(target.transform.position * bulletSpeed, ForceMode.Impulse);
        // _rb.AddForce(directionToPlayer.normalized * bulletSpeed, ForceMode.Impulse);
    }

    public virtual void Shoot()
    {
        if (cooldownTimer <= 0f)
        {
            //shootAnim.SetBool("OnAction", true);
            bulletSource.Play();
            InstantiateBullet();
            cooldownTimer = shootCooldown;
        }
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target);

        Shoot();
    }

    void Patrolling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = UnityEngine.Random.Range(0, Waypoints.Count);
            //currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Count;
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
