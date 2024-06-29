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

    public List<GameObject> activate = new List<GameObject>();

    void Update()
    {
        if (previousSelectedGun != selectedGun)
            previousSelectedGun = selectedGun;

        if (GameManager.Instance.canAttack)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1)))
            {
                selectedGun = 0;
                foreach(var green in activate)
                {
                    green.SetActive(false);
                }
                if (previousSelectedGun != selectedGun)
                {
                    knife.Play();
                    activate[selectedGun].SetActive(true);
                }
            }
            if ((Input.GetKeyDown(KeyCode.Alpha2)) && transform.childCount >= 2)
            {
                selectedGun = 1;
                foreach(var green in activate)
                {
                    green.SetActive(false);
                }
                if (previousSelectedGun != selectedGun)
                {
                    spoon.Play();
                    activate[selectedGun].SetActive(true);
                }
            }
            if ((Input.GetKeyDown(KeyCode.Alpha3)) && transform.childCount >= 3)
            {
                selectedGun = 2;
                foreach(var green in activate)
                {
                    green.SetActive(false);
                }
                if (previousSelectedGun != selectedGun)
                {
                    shotgun.Play();
                    activate[selectedGun].SetActive(true);
                }
            }
            if ((Input.GetKeyDown(KeyCode.Alpha4)) && transform.childCount >= 4)
            {
                selectedGun = 3;
                foreach(var green in activate)
                {
                    green.SetActive(false);
                }
                if (previousSelectedGun != selectedGun)
                {
                    grenade.Play();
                    activate[selectedGun].SetActive(true);
                }
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