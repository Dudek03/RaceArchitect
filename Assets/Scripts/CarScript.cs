using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[RequireComponent(typeof(Rigidbody))]
public class CarScript : MonoBehaviour
{
    public float force = 10f; //Controls velocity multiplier
    public float maxSpeed = 10f; //Controls velocity multiplier
    public float deltaSpeed = 5f;
    private float currentSpeed = 10f;
    public AnimationCurve speedToForce;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private LayerMask m_WhatIsCeil;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilCheck;
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI speedometerNumber;
    const float k_GroundedRadius = .2f;
    const float k_CeilRadius = .2f;
    private bool m_Grounded;
    Rigidbody rb;
    public Animator animator;

    public bool upArrowActivate = false;
    public int maxTimeUpActivation = 1;
    private float timeUpActivation = 0;
    private Vector3 startPos;
    private Quaternion startRot;
    public ParticleSystem ps;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        if (upArrowActivate)
        {
            timeUpActivation -= Time.deltaTime;
            if (timeUpActivation <= 0)
            {
                upArrowActivate = false;
            }
        }

        m_Grounded = false;

        Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                GameManager.Instance.ResetMultiply();
            }
        }

        if (!m_Grounded)
        {
            GameManager.Instance.AddPoints(
                (int)Math.Ceiling(GameManager.Instance.pointsMultiplication.pointsInAirTime * Time.deltaTime));
        }

        Collider[] colliders2 = Physics.OverlapSphere(m_CeilCheck.position, k_CeilRadius, m_WhatIsCeil);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (!colliders2[i].isTrigger && colliders2[i].gameObject != gameObject)
            {
                GameManager.Instance.GameOver();
            }
        }

        if (GameManager.Instance.gameState != GameState.RUN)
        {
            return;
        }

        rb.AddForce(force * Time.deltaTime * speedToForce.Evaluate(1 - (rb.velocity.sqrMagnitude / currentSpeed)) *
        Vector3.right);

        fill.fillAmount = ((float)currentSpeed / maxSpeed) / 2;
        speedometerNumber.text = $"{(int)currentSpeed} km/h";
    }


    public void ActionRight()
    {
        upArrowActivate = false;
        if (!m_Grounded)
        {
            animator.SetTrigger("frontflip");
            return;
        }

        currentSpeed += deltaSpeed;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public void ActionLeft()
    {
        upArrowActivate = false;
        if (!m_Grounded)
        {
            animator.SetTrigger("backflip");
            return;
        }

        currentSpeed -= deltaSpeed;
        if (currentSpeed <= 0)
        {
            //TODO: LATER 
            Die();
        }
    }

    public void Die()
    {
        ps.Play();

        Vector3 dir = Random.insideUnitCircle.normalized;
        if (dir.y < 0)
        {
            dir = new Vector3(dir.x, -dir.y, dir.z);
        }
        rb.freezeRotation = false;
        rb.AddTorque(Random.insideUnitCircle.normalized * 200);
        rb.AddForce(dir * 10000);
        currentSpeed = 10;
        StartCoroutine(AfterDed());
    }

    IEnumerator AfterDed()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.3f);
        rb.freezeRotation = true;

        GameManager.Instance.GameOver();
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        rb.velocity = Vector3.zero;
    }

    public void ActionUp()
    {
        upArrowActivate = true;
        timeUpActivation = maxTimeUpActivation;
    }

    public void ActionDown()
    {
        upArrowActivate = false;
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public void IncreaseSpeed(float deltaSpeed)
    {
        currentSpeed += deltaSpeed;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
    }
}