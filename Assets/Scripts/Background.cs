using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Background : MonoBehaviour{
    public Transform Cam;
    public Transform Background_obj;
    public Transform Background_clone;

    void Update(){
        float distance = Vector3.Distance(Cam.position, Background_obj.position);
        if(distance<63)
        {
            Background_clone = Instantiate(Background_obj);
            Background_clone.transform.position = new Vector3(Background_obj.transform.position.x + 63, Background_obj.transform.position.y, Background_obj.transform.position.z);
            Background_obj = Background_clone;
        }
    }
}