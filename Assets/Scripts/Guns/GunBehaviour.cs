using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    protected Pool<GameObject> poolBullet;
    protected List<GameObject> bulletObjects = new List<GameObject>();
    int maxBullet;
    [SerializeField] protected Transform bulletPoint;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected Transform camTarget;

    protected float shootCooldown = 3f;
    protected float currentCooldown = 0f;

    public static GunBehaviour Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        currentCooldown = shootCooldown;
    }

    void Start()
    {
        maxBullet = 5;

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
        bullet.GetComponent<Damage>().gb = this;

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

    public virtual void InstantiateBullet()
    {
        
    }

    public virtual void InstantiateBullet(Transform spawn)
    {

    }

    public virtual void Shoot()
    {
        
    }

    public void EndAttackAnim()
    {
        GameManager.Instance.canAttack = true;
    }
}
