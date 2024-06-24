using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    private int damage = 20;
    [SerializeField] AudioSource damageSound;

    [SerializeField] AudioSource knifeSound;
    [SerializeField] Animator KnifeAnimator;
    private float knifeCooldown = 1.5f;
    private float currentCooldown = 0f;

    private bool isAnimFinished;

    private void Awake()
    {
        currentCooldown = knifeCooldown;
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            knifeSound.Play();
            KnifeAnimator.SetTrigger("OnAction");
            currentCooldown = knifeCooldown;
        }
    }

    public void EndAttackAnim()
    {
        GameManager.Instance.canAttack = true;

        if (GameManager.Instance.enemyAttacked)
            GameManager.Instance.enemyAttacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyBeh = other.GetComponent<EnemyBehaviour>();

            if (enemyBeh != null && !GameManager.Instance.enemyAttacked)
            {
                GameManager.Instance.enemyAttacked = true;
                damageSound.Play();
                enemyBeh.TakeDamage(damage);
            }
        }
    }
}
