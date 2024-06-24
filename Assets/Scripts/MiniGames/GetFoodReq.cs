using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GetFoodReq : MonoBehaviour
{
    public FoodType foodType { get; private set; }
    public Dictionary<FoodType, GameObject> food = new Dictionary<FoodType, GameObject>();

    [SerializeField] GameObject tomatoImg;
    [SerializeField] GameObject lettuceImg;
    [SerializeField] GameObject oliveImg;
    [SerializeField] GameObject eggImg;
    [SerializeField] GameObject onionImg;

    [SerializeField] TextMeshProUGUI timerText;
    private int currentTime = 30;

    [SerializeField] TextMeshProUGUI choiceCounterText;
    private int choiceCounter;

    void Awake()
    {
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
        Debug.Log("Mal Chef");
    }

    private void GoodChef(object sender, EventArgs e)
    {
        Debug.Log("Buen Chef");
    }

    private void WrongChoice(object sender, EventArgs e)
    {
        Debug.Log("Incorrecto!");
        if (choiceCounter > 0)
            choiceCounter--;
        choiceCounterText.text = choiceCounter.ToString() + " / 30";
        foodType = GetFood();
        ChangeImage(foodType);
    }

    private void RightChoice(object sender, EventArgs e)
    {
        Debug.Log("Correcto!");
        choiceCounter++;
        choiceCounterText.text = choiceCounter.ToString() + " / 30";
        foodType = GetFood();
        ChangeImage(foodType);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void Start()
    {
        choiceCounter = 0;
        choiceCounterText.text = choiceCounter.ToString() + " / 30";
        StartCoroutine(UpdateTimer());
        foodType = GetFood();
        ChangeImage(foodType);

        food.Add(FoodType.Tomato, tomatoImg);
        food.Add(FoodType.Lettuce, lettuceImg);
        food.Add(FoodType.OliveOil, oliveImg);
        food.Add(FoodType.Egg, eggImg);
        food.Add(FoodType.Onion, onionImg);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (PlayerChoose() == foodType)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnRightChoice, this, EventArgs.Empty);

            if (choiceCounter == 30)
            {
                EventManager.Instance.Dispatch(GameEventTypes.MixGoodChef, this, EventArgs.Empty);
            }
        }

        if (PlayerChoose() != null && PlayerChoose() != foodType)
        {
            EventManager.Instance.Dispatch(GameEventTypes.OnWrongChoice, this, EventArgs.Empty);
        }

        if (currentTime <= 0)
        {
            if (choiceCounter < 30)
                EventManager.Instance.Dispatch(GameEventTypes.MixBadChef, this, EventArgs.Empty);
            StopCoroutine(UpdateTimer());
            timerText.text = "0";
        }
    }

    IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            timerText.text = currentTime.ToString();
            yield return new WaitForSeconds(1);
            currentTime--;
        }
    }

    private FoodType? PlayerChoose()
    {
        FoodType? choice = Choice.Instance.PlayerChoose();
        return choice;
    }

    FoodType GetFood()
    {
        Array values = Enum.GetValues(typeof(FoodType));
        System.Random random = new System.Random();
        FoodType randomFood = (FoodType)values.GetValue(random.Next(values.Length));
        Debug.Log(randomFood);
        return randomFood;
    }

    void ChangeImage(FoodType foodType)
    {
        tomatoImg.SetActive(foodType == FoodType.Tomato ? true : false);
        lettuceImg.SetActive(foodType == FoodType.Lettuce ? true : false);
        oliveImg.SetActive(foodType == FoodType.OliveOil ? true : false);
        eggImg.SetActive(foodType == FoodType.Egg ? true : false);
        onionImg.SetActive(foodType == FoodType.Onion ? true : false);
    }
}

public enum FoodType
{
    Tomato,
    Lettuce,
    OliveOil,
    Egg,
    Onion
}