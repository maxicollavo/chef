using System;
using UnityEngine;

public class LeaveIngredient : MonoBehaviour
{
    private float recipeIng = 1f;
    private float chefIng = .5f;

    [SerializeField] private GameObject chefIngredient;
    [SerializeField] private GameObject recipeIngredient;


    //private void Awake()
    //{
    //    EventManager.Instance.Register(GameEventTypes.OnLeaveChefIngredient, OnLeaveChefIngredient);
    //    EventManager.Instance.Register(GameEventTypes.OnLeaveRecipeIngredient, OnLeaveRecipeIngredient);
    //}

    //private void OnDestroy()
    //{
    //    EventManager.Instance.Unregister(GameEventTypes.OnLeaveChefIngredient, OnLeaveChefIngredient);
    //    EventManager.Instance.Unregister(GameEventTypes.OnLeaveRecipeIngredient, OnLeaveRecipeIngredient);
    //}

    //private void OnLeaveRecipeIngredient(object sender, EventArgs e)
    //{
    //    Instantiate(recipeIngredient, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}

    //private void OnLeaveChefIngredient(object sender, EventArgs e)
    //{
    //    Instantiate(chefIngredient, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}
}
