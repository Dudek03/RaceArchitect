using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarScript : MonoBehaviour
{
    public float force = 10f; //Controls velocity multiplier
    public float speed = 10f; //Controls velocity multiplier
    public AnimationCurve speedToForce;

    Rigidbody rb; //Tells script there is a rigidbody, we can use variable rb to reference it in further script


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //rb equals the rigidbody on the player
    }


    // Update is called once per frame
    void Update()
    {
        rb.AddForce( force * Time.deltaTime * speedToForce.Evaluate(1 - (rb.velocity.sqrMagnitude / speed)) * Vector3.right );

    }
}