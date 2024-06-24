using System;
using UnityEngine;

public class EnemyRecipe : MonoBehaviour
{
    public GunType gunType { get; set; }
    public int hitNumber { get; set; }

    [SerializeField] private GameObject ingredient;

    public bool pactAchieved;

    public static EnemyRecipe Instance { get; private set; }


    private void Awake()
    {
        Instance = this;

        hitNumber = UnityEngine.Random.Range(1, 4);
        gunType = GetGun();
    }

    public GunType GetGun()
    {
        Array values = Enum.GetValues(typeof(GunType));
        System.Random random = new System.Random();
        GunType randomgun = (GunType)values.GetValue(random.Next(values.Length));
        return randomgun;
    }

    void CheckPact()
    {
        hitNumber--;

        if (hitNumber <= 0)
        {
            pactAchieved = true;
        }

        if (pactAchieved)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnAbility, this, EventArgs.Empty);
            GameManager.Instance.enemies.Remove(gameObject);

            if (GameManager.Instance.readyToInstantiate)
            {
                Instantiate(ingredient, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gunType == GunType.Knife)
        {
            if (other.gameObject.CompareTag("Knife"))
            {
                CheckPact();
            }
        }
        else if (gunType == GunType.Spoon)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                CheckPact();
            }
        }
    }
}