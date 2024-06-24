using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool<T>
{
    Func<T> _factoryMethod; 
    List<T> _currentStock;
    Action<T> _turnOnCallback;
    Action<T> _turnOffCallback;

    public Pool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialAmount)
    {
        _currentStock = new List<T>();
        _factoryMethod = factoryMethod;
        _turnOnCallback = turnOnCallback;
        _turnOffCallback = turnOffCallback;

        for (int i = 0; i < initialAmount; i++)
        {
            T newObj = _factoryMethod(); 
            _turnOffCallback(newObj);
            _currentStock.Add(newObj);
        }
    }

    public T GetObject()
    {
        T result;

        if (_currentStock.Count == 0)
        {
            result = _factoryMethod();
        }
        else
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
        }

        _turnOnCallback(result);
        return result;
    }

    public void ReturnObject(T obj)
    {
        _turnOffCallback(obj);
        _currentStock.Add(obj);
    }
}
