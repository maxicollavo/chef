using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawn : FallingFood
{
    public override void  InstantiateFruit()
    {
        GameObject fruitInstance = poolFruit.GetObject();
        fruitInstance.transform.position = transform.position;
        fruitObjects.Add(fruitInstance);

        var rb = fruitInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.left * 100;

        fruitCount++;
    }
}