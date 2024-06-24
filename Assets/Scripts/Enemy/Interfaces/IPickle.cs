using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickle
{
    public void TakeDamage(ref int playerHealth);
    public void ResetCounter();
}
