using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour
{
    public float time2Launch = 2f;
    public Vector3 force;
    public Vector3 lforce;
    public float superJumpMultiplication = 2;
    private float timer = 0;
    private bool start_launch = false;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!start_launch) return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Launch();
            timer = 0;
            start_launch = false;
        }
    }

    void Launch()
    {
        if (GameManager.Instance.gameState != GameState.RUN) return;
        sound.Play();
        if (GameManager.Instance.car.upArrowActivate)
        {
            GameManager.Instance.car.ApplyForce(force * superJumpMultiplication);
        }

        if (GameManager.Instance.car.leftArrowActivate)
        {
            GameManager.Instance.car.ApplyForce(lforce);
        }
        else
        {
            GameManager.Instance.car.ApplyForce(force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            start_launch = true;
            timer = time2Launch;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!start_launch) return;
        if (other.gameObject == GameManager.Instance.car.gameObject)
        {
            Launch();
            timer = 0;
            start_launch = false;
        }
    }
}
