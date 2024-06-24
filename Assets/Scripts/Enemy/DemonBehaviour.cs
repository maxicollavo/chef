using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehaviour : MonoBehaviour
{
    public Material material;
    private Renderer renderer;
    private EnemyRecipe enemyRecipe;

    void Start()
    {
        enemyRecipe = transform.parent.GetComponent<EnemyRecipe>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (enemyRecipe.pactAchieved)
        {
            renderer.material = material;
        }
    }
}
