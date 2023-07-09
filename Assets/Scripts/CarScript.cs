using System;
using System.Collections;
using System.Collections.Generic;
using blocks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class CarScript : MonoBehaviour
{
    public float force = 10f; //Controls velocity multiplier
    public Vector3 force_slam; //Slam force
    public Vector3 rdash;
    public Vector3 ldash;
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
    public bool upOnGround = false;
    public int maxUpOnGroundTime = 3;
    private float upOnGroundTime = 0;

    public bool downArrowActivate = false;
    public bool slam = false;
    public int maxTimeDownActivation = 3;
    private float timeDownActivation = 0;

    public bool leftArrowActivate = false;
    public int maxTimeLeftActivation = 1;
    private float timeLeftActivation = 0;

    public bool rightArrowActivate = false;
    public int maxTimeRightActivation = 3;
    private float timeRightActivation = 0;

    private Vector3 startPos;
    private Quaternion startRot;
    public ParticleSystem ps;
    public AnimationCurve winTimeSlowdown;
    public AnimationCurve dedTimeSlowdown;

    private float timeAnimation = 0;
    public float flipAnimation = 0.3f;
    public float timeSlowdownDuration;

    void Start()
    {
        slam = false;
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
    }


    // Update is called once per frame
    void Update()
    {
        timeAnimation -= Time.deltaTime;
        if (upArrowActivate)
        {
            timeUpActivation -= Time.deltaTime;
            if (timeUpActivation <= 0)
            {
                upArrowActivate = false;
            }
        }

        if (upOnGround == true)
        {
            GameManager.Instance.AddPoints(
                (int)Math.Ceiling(GameManager.Instance.pointsMultiplication.pointsOnGround * Time.deltaTime));
            upOnGroundTime -= Time.deltaTime;
            if (upOnGroundTime <= 0)
            {
                upOnGround = false;
            }
        }

        if (downArrowActivate)
        {
            timeDownActivation -= Time.deltaTime;
            if (timeDownActivation <= 0)
            {
                downArrowActivate = false;
            }
        }

        if (leftArrowActivate)
        {
            timeLeftActivation -= Time.deltaTime;
            if (timeLeftActivation <= 0)
            {
                leftArrowActivate = false;
            }
        }

        if (rightArrowActivate)
        {
            timeRightActivation -= Time.deltaTime;
            if (timeRightActivation <= 0)
            {
                rightArrowActivate = false;
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
                (int)Math.Ceiling(GameManager.Instance.pointsMultiplication.pointsInAirTime * Time.deltaTime *
                                  currentSpeed * GameManager.Instance.pointsMultiplication.speedMultiply));
        }

        Collider[] colliders2 = Physics.OverlapSphere(m_CeilCheck.position, k_CeilRadius, m_WhatIsCeil);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (!colliders2[i].isTrigger && colliders2[i].gameObject != gameObject)
            {
                Die();
            }
        }

        if (GameManager.Instance.gameState != GameState.RUN)
        {
            return;
        }

        rb.AddForce(force * Time.deltaTime * speedToForce.Evaluate(1 - (rb.velocity.sqrMagnitude / currentSpeed)) *
                    Vector3.right);

        fill.fillAmount = ((float)currentSpeed / maxSpeed) / 2;
        speedometerNumber.text = $"{(int)currentSpeed}";
    }


    public void ActionRight()
    {
        upArrowActivate = false;
        downArrowActivate = false;
        leftArrowActivate = false;
        rightArrowActivate = true;
        timeRightActivation = maxTimeRightActivation;
        if (!m_Grounded && timeAnimation < 0)
        {
            print("FLIP");
            GameManager.Instance.AddMultiply(GameManager.Instance.pointsMultiplication.frontFlipIncrease);
            timeAnimation = flipAnimation;
            animator.SetTrigger("frontflip");
            return;
        }
        if (m_Grounded)
        {
            Debug.Log("me");
            GameManager.Instance.car.ApplyForce(rdash);
        }
    }

    public void ActionLeft()
    {
        upArrowActivate = false;
        downArrowActivate = false;
        leftArrowActivate = true;
        rightArrowActivate = false;
        timeLeftActivation = maxTimeLeftActivation;
        if (!m_Grounded && timeAnimation < 0)
        {
            GameManager.Instance.AddMultiply(GameManager.Instance.pointsMultiplication.backFlipIncrease);
            timeAnimation = flipAnimation;
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

    public void Win()
    {
        if (GameManager.Instance.gameState == GameState.DEATH || GameManager.Instance.gameState == GameState.WINLOSE) return;
        currentSpeed = 10;
        rb.velocity = Vector3.zero;
        GameManager.Instance.gameState = GameState.WINLOSE;
        StartCoroutine(AfterWin());
    }


    IEnumerator AfterWin()
    {
        yield return SlowDownTime(winTimeSlowdown);
        UiManager.Instance.ShowWin();
    }

    public void Rot_reset()
    {
        transform.rotation = startRot;
    }
    public void Die()
    {
        if (GameManager.Instance.gameState != GameState.RUN) return;
        GameManager.Instance.gameState = GameState.DEATH;
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

    IEnumerator SlowDownTime(AnimationCurve timeSlowdown)
    {
        float time = 0;
        while (time < timeSlowdownDuration)
        {
            Time.timeScale = timeSlowdown.Evaluate(time / timeSlowdownDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AfterDed()
    {
        yield return new WaitForSeconds(2);
        yield return SlowDownTime(dedTimeSlowdown);
        GameManager.Instance.GameOver();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Time.timeScale = 1;
        currentSpeed = 10f;
    }

    public void ActionUp()
    {
        upArrowActivate = true;
        downArrowActivate = false;
        leftArrowActivate = false;
        rightArrowActivate = false;
        timeUpActivation = maxTimeUpActivation;
        if (!m_Grounded)
        {
            Rot_reset();
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        }

        if (m_Grounded)
        {
            upOnGround = true;
            upOnGroundTime = maxUpOnGroundTime;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void ActionDown()
    {
        upArrowActivate = false;
        downArrowActivate = true;
        leftArrowActivate = false;
        rightArrowActivate = false;
        if (!m_Grounded)
        {
            GameManager.Instance.car.ApplyForce(force_slam);
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
            slam = true;
        }
        timeDownActivation = maxTimeDownActivation;
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
