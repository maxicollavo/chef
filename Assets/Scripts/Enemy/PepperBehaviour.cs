using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PepperBehaviour : EnemyAI
{
     int BombDMG = 50;
    public AudioSource BombAudio;

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
            PlayerBehaviour _pb = other.transform.parent.GetComponent<PlayerBehaviour>();
             _pb.TakeDamage(BombDMG);
        Destroy(this.gameObject);
    }

    public override void Shoot()
    {
        Debug.Log("");
    }
}
