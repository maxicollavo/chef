using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public KeyCode changeKey = KeyCode.Q;
    [SerializeField] GameObject bookHolder;
    void Update()
    {
        if (Input.GetKeyDown(changeKey))
        {
            if (bookHolder.activeInHierarchy)
                GameManager.Instance.hasRecipeBook = !GameManager.Instance.hasRecipeBook;
        }
    }
}
