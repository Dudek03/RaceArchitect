using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public ParticleSystem[] particles;
    private void OnTriggerEnter(Collider other)
    {
        other.BroadcastMessage("Win");
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
