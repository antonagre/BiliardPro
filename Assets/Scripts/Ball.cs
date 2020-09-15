using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour {
    public Rigidbody rb;
    public Vector3 startPos;


    public abstract void inBuca();
    
    public bool isMoving() {
        if (rb.velocity != Vector3.zero) return true;
        else return false;
    }

    public void init() {
        rb = GetComponent<Rigidbody>();
        startPos = rb.transform.position;
    }

}
