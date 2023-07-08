using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public ParticleSystem[] particles;

    public Transform posMax1;
    public Transform posMax2;

    private void Start()
    {
        float x = Mathf.Round(Random.Range(posMax1.position.x, posMax2.position.x));
        float y = Mathf.Round(Random.Range(posMax1.position.y, posMax2.position.y));
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.points < GameManager.Instance.targetGame) return;
        other.BroadcastMessage("Win");
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
