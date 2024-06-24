using System;
using UnityEngine;
using UnityEngine.UI;
public class Bar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public float currentValue = 50;
    public float maxValue = 100;
    public float points = 5;
    public bool goodChef;
    public bool neutralBar;
    public static Bar Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    //void Start()
    //{
    //    UpdateBar(currentValue, maxValue);
    //    EventManager.Instance.Register(GameEventTypes.OnChef, ChefDecision);
    //    EventManager.Instance.Register(GameEventTypes.OnRecipe, RecipeDecision);
    //}

    //private void OnDestroy()
    //{
    //    EventManager.Instance.Unregister(GameEventTypes.OnChef, ChefDecision);
    //    EventManager.Instance.Unregister(GameEventTypes.OnRecipe, RecipeDecision);
    //}

    private void ChefDecision(object sender, EventArgs e)
    {
        if (currentValue >= 0 && currentValue < maxValue)
        {
            currentValue += points;
            UpdateBar(currentValue, maxValue);
        }
    }

    private void RecipeDecision(object sender, EventArgs e)
    {
        if (currentValue > 0 && currentValue <= maxValue)
        {
            currentValue -= points;
            UpdateBar(currentValue, maxValue);
        }
    }
    
    public void UpdateBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}