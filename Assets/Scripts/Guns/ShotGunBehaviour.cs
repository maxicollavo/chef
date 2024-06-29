using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    Pool<GameObject> poolBullet;
    private List<GameObject> bulletObjects = new List<GameObject>();
    private int maxBullet;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform bulletPoint2;
    [SerializeField] private Transform bulletPoint3;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioSource bulletSource;
    [SerializeField] private Animator spoonAnimator;
    [SerializeField] private Transform camTarget;

    private bool isShooting;
    private float shootCooldown = 3f;
    private float currentCooldown = 0f;

    public static ShotgunBehaviour Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        currentCooldown = shootCooldown;
    }

    void Start()
    {
        maxBullet = 9;

        poolBullet = new Pool<GameObject>(CreateBullet, (gameObject) => gameObject.SetActive(true), (gameObject) => gameObject.SetActive(false), maxBullet);
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
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Damage>().sg = this;

        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        poolBullet.ReturnObject(bullet);
        bulletObjects.Remove(bullet);
    }

    public void ResetPool()
    {
        bulletObjects.ForEach((gameObject) => ReturnBullet(gameObject));
    }

    void InstantiateBullet(Transform point)
    {
        GameObject bulletInstance = poolBullet.GetObject();
    bulletInstance.transform.position = point.position;
    bulletObjects.Add(bulletInstance);

    Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
    rb.velocity = Vector3.zero;

    Vector3 shootDirection = camTarget.forward;
    shootDirection.x += UnityEngine.Random.Range(-0.1f, 0.1f);
    shootDirection.y += UnityEngine.Random.Range(-0.1f, 0.1f);
    shootDirection.z += UnityEngine.Random.Range(-0.1f, 0.1f);
    shootDirection.Normalize();

    rb.AddForce(shootDirection * bulletSpeed, ForceMode.Force);
    }

    public void Shoot()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            spoonAnimator.SetTrigger("OnAction");
            InstantiateBullet(bulletPoint);
            InstantiateBullet(bulletPoint2);
            InstantiateBullet(bulletPoint3);
            bulletSource.Play();
            currentCooldown = shootCooldown;
        }
    }

    public void EndAttackAnim()
    {
        GameManager.Instance.canAttack = true;
    }
}
