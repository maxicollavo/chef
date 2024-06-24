using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public Dictionary<GunType, GameObject> guns = new Dictionary<GunType, GameObject>();

    [SerializeField]
    GameObject knifeImg;

    [SerializeField]
    GameObject spoonImg;

    [SerializeField]
    private TextMeshProUGUI showRequestText;

    void Start()
    {
        guns.Add(GunType.Knife, knifeImg);
        guns.Add(GunType.Spoon, spoonImg);

        CheckEnemyRecipeBook();
    }

    private void Update()
    {
        CheckEnemyRecipeBook();
    }

    void ChangeImage(GunType gunType)
    {
        knifeImg.SetActive(gunType == GunType.Knife ? true : false);
        spoonImg.SetActive(gunType == GunType.Spoon ? true : false);
    }

    public void CheckEnemyRecipeBook()
    {
        EnemyRecipe enemyRecipe = transform.parent.parent.gameObject.GetComponent<EnemyRecipe>();

        if (enemyRecipe != null)
        {
            var hits = enemyRecipe.hitNumber;
            var gun = enemyRecipe.gunType;
            ChangeImage(gun);
            showRequestText.text = "" + hits;
        }
    }
}

public enum GunType
{
    Knife,
    Spoon
}
