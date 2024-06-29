using System.Collections.Generic;
using UnityEngine;

public class GranadeBehaviour : GunBehaviour
{
    [SerializeField] AudioSource bulletSource;
    [SerializeField] Animator grenadeAnimator;

    public override void InstantiateBullet()
    {
        GameObject bulletInstance = poolBullet.GetObject();
        bulletInstance.transform.position = bulletPoint.position;
        bulletObjects.Add(bulletInstance);

        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Vector3 launchDirection = (camTarget.forward + Vector3.up * 0.1f).normalized;
        rb.AddForce(launchDirection * bulletSpeed, ForceMode.Impulse);
    }

    public override void Shoot()
    {
        if (GameManager.Instance.canAttack)
        {
            GameManager.Instance.canAttack = false;
            grenadeAnimator.SetTrigger("OnAction");
            InstantiateBullet();
            bulletSource.Play();
            currentCooldown = shootCooldown;
        }
    }
}
