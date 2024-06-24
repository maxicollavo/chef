using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickleDamage : MonoBehaviour, IPickle
{
    private int pickleDmg = 25;
    private float counter = 0;
    private float maxCounter = 3;

    public void ResetCounter()
    {
        counter = 0;
    }

    public void TakeDamage(ref int playerHealth)
    {
        counter += Time.deltaTime;
        if (counter >= maxCounter)
        {
            playerHealth -= pickleDmg;
            if (playerHealth <= 0)
            {
                SceneManager.LoadScene("LoseMenu");
            }
            counter = 0;
        }
    }
}
