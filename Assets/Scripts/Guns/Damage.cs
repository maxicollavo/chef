using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damage = 20;
    [SerializeField] AudioSource damageSound;
    public SpoonBehaviour sb { get; set; }
    public ShotGunBehaviour sg { get; set; }

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
            sb.ReturnBullet(gameObject);
            sg.ReturnBullet(gameObject);
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

                sb.ReturnBullet(gameObject);
                sg.ReturnBullet(gameObject);    
            }
        }
    }
}