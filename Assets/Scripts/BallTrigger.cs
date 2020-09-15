using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
        public void OnTriggerEnter(Collider collider) {
            GameObject other = collider.gameObject;
            other.GetComponent<Ball>().inBuca();
        }
}
