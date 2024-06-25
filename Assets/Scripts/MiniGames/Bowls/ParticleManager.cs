using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    public void StartParticle()
    {
        particle.Play();
    }

    public void StopParticle()
    {
        particle.Stop();
    }
}
