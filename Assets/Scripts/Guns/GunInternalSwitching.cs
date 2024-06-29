using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GunInternalSwitching : MonoBehaviour
{
    [SerializeField] GameObject knife1GO;
    [SerializeField] GameObject knife2GO;
    [SerializeField] GameObject spoon1GO;

    [SerializeField] GameObject spoon2GO;

    [SerializeField] GameObject pan1GO;

    [SerializeField] GameObject pan2GO;

    [SerializeField] GameObject rollo1GO;
    [SerializeField] GameObject rollo2GO;
    
    [SerializeField] GameObject shotgun1GO;
    [SerializeField] GameObject shotgun2GO;
    
    GameObject holdingGun;


    public Dictionary<GunsTypes, GameObject> guns = new Dictionary<GunsTypes, GameObject>();

     void Start()
    {
        guns.Add(GunsTypes.Knife1, knife1GO);
        guns.Add(GunsTypes.Knife2, knife2GO);

        guns.Add(GunsTypes.Spoon1, spoon1GO);
        guns.Add(GunsTypes.Spoon2, spoon2GO);

        guns.Add(GunsTypes.Pan1, pan1GO);
        guns.Add(GunsTypes.Pan2, pan2GO);

        guns.Add(GunsTypes.Rollo1, rollo1GO);
        guns.Add(GunsTypes.Rollo2, rollo2GO);
        
        guns.Add(GunsTypes.ShotGun1, shotgun1GO);
        guns.Add(GunsTypes.ShotGun2, shotgun2GO);
    }
    public void SetInternalGunActive(GunsTypes type, bool isActive)
    {
        if (guns.ContainsKey(type))
        {
            GameObject gunObject = guns[type];
            gunObject.SetActive(isActive);
        }
    }
}

public enum GunsTypes
{
    Knife1,
    Knife2,
    Spoon1,
    Spoon2,
    Pan1,
    Pan2,
    Rollo1,
    Rollo2,
    ShotGun1,
    ShotGun2
}