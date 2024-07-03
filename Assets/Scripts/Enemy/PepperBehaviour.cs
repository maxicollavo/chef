using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PepperBehaviour : EnemyAI
{
     int BombDMG = 50;
    public AudioSource BombAudio;
    public ParticleSystem BombParticles;
    public ParticleSystem Flash2Particles;
    public ParticleSystem SmokeParticles;
    public ParticleSystem FlashParticles;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            Debug.Log("Entro a trigger");

            StartCoroutine (TimerBomb(other));
        }
    }
    IEnumerator TimerBomb(Collider other)
    {
          Debug.Log("Entro a timer");
          BombAudio.Play();
          yield return new WaitForSeconds(2);
          BombParticles.Play();
          Flash2Particles.Play();
          FlashParticles.Play();
          SmokeParticles.Play();
          yield return new WaitForSeconds(0.1f);
            PlayerBehaviour _pb = other.transform.parent.GetComponent<PlayerBehaviour>();
             _pb.TakeDamage(BombDMG);
        Destroy(this.gameObject);
    }

    public override void Shoot()
    {
        Debug.Log("");
    }
}
