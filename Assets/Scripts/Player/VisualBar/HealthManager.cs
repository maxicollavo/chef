using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [SerializeField] List<GameObject> healthVisual = new List<GameObject>();
    [SerializeField] TextMeshProUGUI healthTMP;
    [SerializeField] PlayerBehaviour playerBehaviour;

    private void Update()
    {
        switch (playerBehaviour.health)
        {
            case 100:
                for (int i = 0; i < healthVisual.Count; i++)
                    healthVisual[i].SetActive(true);
                healthTMP.text = "100%";
                break;
                case 95:
                healthTMP.text = "95%";
                break;
            case 90:
                healthTMP.text = "90%";
                break;
                case 85:
                healthTMP.text = "85%";
                break;
            case 80:
                healthVisual[0].SetActive(false);
                for (int i = 1; i < healthVisual.Count; i++)
                    healthVisual[i].SetActive(true);
                healthTMP.text = "80%";
                break;
                case 75:
                healthTMP.text = "75%";
                break;
            case 70:
                healthTMP.text = "70%";
                break;
                case 65:
                healthTMP.text = "65%";
                break;
            case 60:
                healthVisual[1].SetActive(false);
                for (int i = 2; i < healthVisual.Count; i++)
                    healthVisual[i].SetActive(true);
                healthTMP.text = "60%";
                break;
                case 55:
                healthTMP.text = "55%";
                break;
            case 50:
                healthTMP.text = "50%";
                break;
                case 45:
                healthTMP.text = "45%";
                break;
            case 40:
                healthVisual[2].SetActive(false);
                for (int i = 3; i < healthVisual.Count; i++)
                    healthVisual[i].SetActive(true);
                healthTMP.text = "40%";
                break;
                case 35:
                healthTMP.text = "35%";
                break;
            case 30:
                healthTMP.text = "30%";
                break;
                case 24:
                healthTMP.text = "24%";
                break;
            case 20:
                healthVisual[3].SetActive(false);
                for (int i = 4; i < healthVisual.Count; i++)
                    healthVisual[i].SetActive(true);
                healthTMP.text = "20%";
                break;
                case 15:
                healthTMP.text = "15%";
                break;
            case 10:
                healthTMP.text = "10%";
                break;
                case 5:
                healthTMP.text = "5%";
                break;
            default:
                break;
        }
    }
}
