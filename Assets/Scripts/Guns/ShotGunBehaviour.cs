using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehaviour : GunBehaviour
{
    [SerializeField] Transform bulletPoint2;
    [SerializeField] Transform bulletPoint3;
    [SerializeField] AudioSource bulletSource;
    [SerializeField] Animator shotgunAnimator;


    public override void InstantiateBullet(Transform point)
    {
        GameObject bulletInstance = poolBullet.GetObject();
        bulletInstance.transform.position = point.position;
        bulletObjects.Add(bulletInstance);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Vector3 shootDirection = camTarget.forward;
        shootDirection.x += UnityEngine.Random.Range(-0.1f, 0.1f);
        shootDirection.y += UnityEngine.Random.Range(-0.1f, 0.1f);
        shootDirection.z += UnityEngine.Random.Range(-0.1f, 0.1f);
        shootDirection.Normalize();

        rb.AddForce(shootDirection * bulletSpeed, ForceMode.Force);
    }

    public override void Shoot()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            shotgunAnimator.SetTrigger("OnAction");
            InstantiateBullet(bulletPoint);
            InstantiateBullet(bulletPoint2);
            InstantiateBullet(bulletPoint3);
            bulletSource.Play();
            currentCooldown = shootCooldown;
        }
    }
}
