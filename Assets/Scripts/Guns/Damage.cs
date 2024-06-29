using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damage = 20;
    [SerializeField] AudioSource damageSound;
    public SpoonBehaviour sb { get; set; }
    public ShotGunBehaviour sg { get; set; }
    public GranadeBehaviour gb { get; set; }

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
            if(sb != null)
            sb.ReturnBullet(gameObject);
            if(sg != null)
            sg.ReturnBullet(gameObject);
            if(gb != null)
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

                if(sb != null)
                sb.ReturnBullet(gameObject);
                if(sg != null)
                sg.ReturnBullet(gameObject);
                if(gb != null)
                gb.ReturnBullet(gameObject);
            }
        }
    }
}