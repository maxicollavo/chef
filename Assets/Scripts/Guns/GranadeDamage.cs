using UnityEngine;

public class GranadeDamage : MonoBehaviour
{
        private int damage = 20;
    [SerializeField] AudioSource damageSound;
    public GunBehaviour gb { get; set; }

    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            gb.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyBeh = other.GetComponent<EnemyBehaviour>();

            if (enemyBeh != null)
            {
                damageSound.Play();
                enemyBeh.TakeDamage(damage);

                gb.ReturnBullet(gameObject);
            }
        }
    }
}
