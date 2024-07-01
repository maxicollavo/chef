    using UnityEngine;

public class HotDogBullet : MonoBehaviour
{
    private int hotDogDmg = 5;
    [SerializeField] AudioSource bonk;
    private float timer;
    public float Speed = 10;

    public EnemyAI eai { get; set; }

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            eai.ReturnBullet(gameObject);
        }

        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            PlayerBehaviour _pb = other.transform.parent.GetComponent<PlayerBehaviour>();
            _pb.TakeDamage(hotDogDmg);
            eai.ReturnBullet(gameObject);
        }

        if (other.CompareTag("ShieldCollider"))
        {
            bonk.Play();
            eai.ReturnBullet(gameObject);
        }
    }
}
