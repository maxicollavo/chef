using System;
using System.Collections.Generic;
using UnityEngine;

public class SpoonBehaviour : GunBehaviour
{
    [SerializeField] AudioSource bulletSource;
    [SerializeField] Animator spoonAnimator;

    public override void InstantiateBullet()
    {
        GameObject bulletInstance = poolBullet.GetObject();
        bulletInstance.transform.position = bulletPoint.position;
        bulletObjects.Add(bulletInstance);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        rb.AddForce(camTarget.forward * bulletSpeed, ForceMode.Force);
    }

    public override void Shoot()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            spoonAnimator.SetTrigger("OnAction");
            InstantiateBullet();
            bulletSource.Play();
            currentCooldown = shootCooldown;
        }
    }
}