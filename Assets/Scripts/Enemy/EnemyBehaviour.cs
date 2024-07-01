using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Slider LifeBar;
    [SerializeField] Image fillArea;
    private WaitForSeconds time = new WaitForSeconds(0.2f);
    public Animator anim;

    [SerializeField] private GameObject ingredient;
    private EnemyAI ai;
    private NavMeshAgent nav;

    private void Awake()
    {
        fillArea.color = Color.green;
        ai = GetComponent<EnemyAI>();
        nav = GetComponent<NavMeshAgent>();
        Debug.Log(ai);
        Debug.Log(nav);
    }

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            ai.enabled = false;
            nav.enabled = false;
            anim.SetTrigger("OnDie");
            EventManager.Instance.Dispatch(GameEventTypes.OnConf, this, EventArgs.Empty);

            if (GameManager.Instance.readyToInstantiate)
            {
                Instantiate(ingredient, transform.position, transform.rotation);
            }
            GameManager.Instance.enemies.Remove(gameObject);
        }
        StartCoroutine(BarOnTakeDamage());
        LifeBar.value = health;
    }

    IEnumerator BarOnTakeDamage()
    {
        fillArea.color = Color.red;
        yield return time;
        fillArea.color = Color.green;
    }
}