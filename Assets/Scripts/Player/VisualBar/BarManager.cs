using UnityEngine;
using TMPro;
using System;

public class BarManager : MonoBehaviour
{
    public int currentConfValue = 50;
    public int currentAbValue = 50;
    private int _maxValue = 100;
    private int _points = 10;

    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private TextMeshProUGUI chefText;

    public static BarManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        EventManager.Instance.Register(GameEventTypes.OnConf, OnConfDecision);
        EventManager.Instance.Register(GameEventTypes.OnAbility, OnAbilityDecision);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnConf, OnConfDecision);
        EventManager.Instance.Unregister(GameEventTypes.OnAbility, OnAbilityDecision);
    }

    private void OnAbilityDecision(object sender, EventArgs e)
    {
        if (currentAbValue < _maxValue)
        {
            currentAbValue += _points;
            currentConfValue -= _points;
            chefText.text = currentConfValue.ToString() + "%";
            recipeText.text = currentAbValue.ToString() + "%";
        }
    }

    private void OnConfDecision(object sender, EventArgs e)
    {
        if (currentConfValue < _maxValue)
        {
            currentConfValue += _points;
            currentAbValue -= _points;
            recipeText.text = currentAbValue.ToString() + "%";
            chefText.text = currentConfValue.ToString() + "%";
        }
    }
}
