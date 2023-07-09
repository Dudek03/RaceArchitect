using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public ParticleSystem[] particles;


    public void SetPos(Vector3 a, Vector3 b)
    {

        float x = Mathf.Round(Random.Range(a.x, b.x));
        float y = Mathf.Round(Random.Range(a.y, b.y));
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.points < GameManager.Instance.targetGame && GameManager.Instance.currentLevelData.pointsTarget) return;
        other.BroadcastMessage("Win");
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
