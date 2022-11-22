using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out PlayerMovement player)) return;
        player.ReachFinish();

        /*FinishParticleSystem = GameObject.Find("Finish Particle System").GetComponent<ParticleSystem>();
        FinishParticleSystem.Play();*/
    }
}
