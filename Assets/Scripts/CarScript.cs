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

    Rigidbody rb; //Tells script there is a rigidbody, we can use variable rb to reference it in further script


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //rb equals the rigidbody on the player
        currentSpeed = maxSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState != GameState.RUN) return;
        rb.AddForce(force * Time.deltaTime * speedToForce.Evaluate(1 - (rb.velocity.sqrMagnitude / currentSpeed)) *
                    Vector3.right);
    }


    public void ActionRight()
    {
        currentSpeed += deltaSpeed;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }

    public void ActionLeft()
    {
        currentSpeed -= deltaSpeed;
        if (currentSpeed <= 0)
        {
            //TODO: LATER 
            Debug.Log("YOU LOSE");
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
}