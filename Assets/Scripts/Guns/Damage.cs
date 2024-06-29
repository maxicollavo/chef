using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damage = 20;
    [SerializeField] AudioSource damageSound;
    public SpoonBehaviour sb { get; set; }
<<<<<<< Updated upstream
    public GranadeBehaviour gb { get; set; }
=======
    public ShotGunBehaviour sg { get; set; }
    public GranadeBehaviour gb { get; set; }

>>>>>>> Stashed changes
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
            if (sb != null)
                sb.ReturnBullet(gameObject);
<<<<<<< Updated upstream
=======
            if (sg != null)
                sg.ReturnBullet(gameObject);
>>>>>>> Stashed changes
            if (gb != null)
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
<<<<<<< Updated upstream
                if (sb != null)
                    sb.ReturnBullet(gameObject);
=======

                if (sb != null)
                    sb.ReturnBullet(gameObject);
                if (sg != null)
                    sg.ReturnBullet(gameObject);
>>>>>>> Stashed changes
                if (gb != null)
                    gb.ReturnBullet(gameObject);
            }
        }
    }
}