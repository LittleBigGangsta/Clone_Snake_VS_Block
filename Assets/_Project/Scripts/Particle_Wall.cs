using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Wall : MonoBehaviour
{
    public ParticleSystem Win_Particle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Win_Particle.Play();
        }
    }
}
