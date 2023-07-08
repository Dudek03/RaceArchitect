using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarScript : MonoBehaviour
{
    public float force = 10f; //Controls velocity multiplier
    public float maxSpeed = 10f; //Controls velocity multiplier
    public float deltaSpeed = 5f;
    private float currentSpeed;
    public AnimationCurve speedToForce;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private LayerMask m_WhatIsCeil;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilCheck;
    const float k_GroundedRadius = .2f;
    const float k_CeilRadius = .2f;
    private bool m_Grounded;
    Rigidbody rb;
    public Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = maxSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        m_Grounded = false;

        Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        Collider[] colliders2 = Physics.OverlapSphere(m_CeilCheck.position, k_CeilRadius, m_WhatIsCeil);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].gameObject != gameObject)
            {
                GameManager.Instance.GameOver();
            }
        }
        if (GameManager.Instance.gameState != GameState.RUN) return;

        rb.AddForce(force * Time.deltaTime * speedToForce.Evaluate(1 - (rb.velocity.sqrMagnitude / currentSpeed)) *
                    Vector3.right);
    }


    public void ActionRight()
    {
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
        if (!m_Grounded)
        {
            animator.SetTrigger("backflip");
            return;
        }

        currentSpeed -= deltaSpeed;
        if (currentSpeed <= 0)
        {
            //TODO: LATER 
            GameManager.Instance.GameOver();
            currentSpeed = 0;
        }
    }

    public void ActionUp()
    {
        Debug.Log("NOW I DO NOTHING, but I am Up");
    }

    public void ActionDown()
    {
        Debug.Log("NOW I DO NOTHING, but I am Down");
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}