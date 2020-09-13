using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
        public Transform CenterTable;
        private int n = 0;
        public void OnTriggerEnter(Collider collider) {
            GameObject other = collider.gameObject;
            other.transform.position = CenterTable.position;
            Debug.Log("la palla è caduta abbascio");
            other.GetComponent<Ball>().inBuca();
            n++;
            UI_Manager.Instance.updateCounter(n);
        }
}
