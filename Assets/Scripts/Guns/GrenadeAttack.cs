using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttack : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public int damage = 100;
    public GameObject explosionEffect;

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Mostrar efecto de explosión
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            EnemyBehaviour target = nearbyObject.GetComponent<EnemyBehaviour>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
