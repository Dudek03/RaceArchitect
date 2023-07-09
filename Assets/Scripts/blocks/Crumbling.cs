using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

namespace blocks
{
    public class Crumbling : MonoBehaviour
    {
        public GameObject crumbling_road;
        public int road_destroy = 300;
        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.Instance.car.slam)
            {
                Destroy(crumbling_road);
                GameManager.Instance.progressCounter.UpdatePoints(road_destroy);
                GameManager.Instance.car.slam = false;
                GameManager.Instance.car.Rot_reset();
            }
        }
    }
}
