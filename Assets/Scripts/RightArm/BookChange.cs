using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookChange : MonoBehaviour
{
    [SerializeField] GameObject recipeBookGO;
    public Dictionary<BookType, GameObject> books = new Dictionary<BookType, GameObject>();

    void Start()
    {
        books.Add(BookType.Recipe, recipeBookGO);
    }

    void Update()
    {
        SetBookActive(BookType.Recipe, GameManager.Instance.hasRecipeBook);
    }

    public void SetBookActive(BookType type, bool isActive)
    {
        if (books.ContainsKey(type))
        {
            GameObject bookObject = books[type];
            bookObject.SetActive(isActive);
        }
    }
}

public enum BookType
{
    Recipe
}
