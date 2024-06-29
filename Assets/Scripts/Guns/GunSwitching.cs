using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitching : MonoBehaviour
{
    public int selectedGun = 0;
    private int previousSelectedGun;

    [SerializeField] private AudioSource knife;
    [SerializeField] private AudioSource spoon;
    [SerializeField] private AudioSource shotgun;
    [SerializeField] private AudioSource grenade;

    void Update()
    {
        if (previousSelectedGun != selectedGun)
            previousSelectedGun = selectedGun;

        if (GameManager.Instance.canAttack)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1)))
            {
                selectedGun = 0;
                if (previousSelectedGun != selectedGun)
                    knife.Play();
            }
            if ((Input.GetKeyDown(KeyCode.Alpha2)) && transform.childCount >= 2)
            {
                selectedGun = 1;
                if (previousSelectedGun != selectedGun)
                    spoon.Play();
            }
            if ((Input.GetKeyDown(KeyCode.Alpha3)) && transform.childCount >= 3)
            {
                selectedGun = 2;
                if (previousSelectedGun != selectedGun) 
                    shotgun.Play();
            }
            if ((Input.GetKeyDown(KeyCode.Alpha4)) && transform.childCount >= 4)
            {
                selectedGun = 3;
                if (previousSelectedGun != selectedGun) 
                    grenade.Play();
            }

                if (previousSelectedGun != selectedGun)
                SelectGun();
        }
    }

    public void SelectGun()
    {
        int i = 0;

        foreach (Transform gun in transform)
        {
            if (i == selectedGun)
                gun.gameObject.SetActive(true);
            else
                gun.gameObject.SetActive(false);
            i++;
        }
    }
}