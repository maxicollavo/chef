using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Slider LifeBar;
    [SerializeField] Image fillArea;
    private WaitForSeconds time = new WaitForSeconds(0.2f);
    public Animator dieAnimator;

    [SerializeField] private GameObject ingredient;

    private void Awake()
    {
        fillArea.color = Color.green;
    }

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        dieAnimator.SetTrigger("OnDie");
        health -= damage;

        if (health <= 0)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnConf, this, EventArgs.Empty);

            if (GameManager.Instance.readyToInstantiate)
            {
                Instantiate(ingredient, transform.position, transform.rotation);
            }
            GameManager.Instance.enemies.Remove(gameObject);
            Destroy(gameObject);
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