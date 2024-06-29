using System.Collections.Generic;
using UnityEngine;

public class GranadeBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject granadePrefab;
    Pool<GameObject> poolGranade;
    private List<GameObject> granadeObjects = new List<GameObject>();
    private int maxGranades;
    [SerializeField] private Transform granadePoint;
    [SerializeField] private float granadeSpeed;
    [SerializeField] private AudioSource granadeSource;
    [SerializeField] private Animator spoonAnimator;
    [SerializeField] private Transform camTarget;

    private float shootCooldown = 1f;
    private float currentCooldown = 0f;

    public static GranadeBehaviour Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        currentCooldown = shootCooldown;
    }

    void Start()
    {
        maxGranades = 5;

        poolGranade = new Pool<GameObject>(CreateBullet, (gameObject) => gameObject.SetActive(true), (gameObject) => gameObject.SetActive(false), maxGranades);
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    private GameObject CreateBullet()
    {
        var granade = Instantiate(granadePrefab);
        granade.GetComponent<GranadeDamage>().gb = this;

        return granade;
    }

    public void ReturnBullet(GameObject bullet)
    {
        poolGranade.ReturnObject(bullet);
        granadeObjects.Remove(bullet);
    }

    public void ResetPool()
    {
        granadeObjects.ForEach((gameObject) => ReturnBullet(gameObject));
    }

    void InstantiateBullet()
    {
        GameObject bulletInstance = poolGranade.GetObject();
        bulletInstance.transform.position = granadePoint.position;
        granadeObjects.Add(bulletInstance);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Vector3 launchDirection = (camTarget.forward + Vector3.up * 0.1f).normalized;
        rb.AddForce(launchDirection * granadeSpeed, ForceMode.Impulse);
    }

    public void Shoot()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            spoonAnimator.SetTrigger("OnAction");
            InstantiateBullet();
            granadeSource.Play();
            currentCooldown = shootCooldown;
        }
    }

    public void EndAttackAnim()
    {
        GameManager.Instance.canAttack = true;
    }
}
