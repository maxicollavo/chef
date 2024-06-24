using System;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public static Choice Instance { get; private set; }

    public bool tomato;
    public bool lettuce;
    public bool olive;
    public bool egg;
    public bool onion;

    public bool isChosen;

    void Awake()
    {
        Instance = this;
        EventManager.Instance.Register(GameEventTypes.OnRightChoice, RightChoice);
        EventManager.Instance.Register(GameEventTypes.OnWrongChoice, WrongChoice);

        EventManager.Instance.Register(GameEventTypes.MixGoodChef, GoodChef);
        EventManager.Instance.Register(GameEventTypes.MixBadChef, BadChef);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnRightChoice, RightChoice);
        EventManager.Instance.Unregister(GameEventTypes.OnWrongChoice, WrongChoice);

        EventManager.Instance.Unregister(GameEventTypes.MixGoodChef, GoodChef);
        EventManager.Instance.Unregister(GameEventTypes.MixBadChef, BadChef);
    }

    private void BadChef(object sender, EventArgs e)
    {
        isChosen = true;
    }

    private void GoodChef(object sender, EventArgs e)
    {
        isChosen = true;
    }

    private void WrongChoice(object sender, EventArgs e)
    {
        isChosen = false;
        tomato = false;
        lettuce = false;
        olive = false;
        egg = false;
        onion = false;
    }

    private void RightChoice(object sender, EventArgs e)
    {
        isChosen = false;
        tomato = false;
        lettuce = false;
        olive = false;
        egg = false;
        onion = false;
    }

    public FoodType? PlayerChoose()
    {
        FoodType? foodChoose = null;

        if (tomato)
            foodChoose = FoodType.Tomato;

        if (lettuce)
            foodChoose = FoodType.Lettuce;

        if (olive)
            foodChoose = FoodType.OliveOil;

        if (egg)
            foodChoose = FoodType.Egg;

        if (onion)
            foodChoose = FoodType.Onion;

        return foodChoose;
    }

    public void TomatoChosen()
    {
        if (!isChosen)
        {
            tomato = true;
            isChosen = true;
        }
    }

    public void LettuceChosen()
    {
        if (!isChosen)
        {
            lettuce = true;
            isChosen = true;
        }
    }

    public void OliveChosen()
    {
        if (!isChosen)
        {
            olive = true;
            isChosen = true;
        }
    }

    public void EggChosen()
    {
        if (!isChosen)
        {
            egg = true;
            isChosen = true;
        }
    }

    public void OnionChosen()
    {
        if (!isChosen)
        {
            onion = true;
            isChosen = true;
        }
    }
}
