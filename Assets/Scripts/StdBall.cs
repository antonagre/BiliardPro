using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StdBall : Ball
{
    public int ballNumber;
    private Rigidbody rb;
    private Vector3 startPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        String name = gameObject.name;
        int i = name.IndexOf("_");
        startPos = rb.transform.position;
        ballNumber = Int32.Parse(name.Substring(i+1));
        GameManager.getManager().balls.Add(this);
    }
    
    public void remove() {
        Debug.Log("Ball "+ballNumber+" destroyed.");
        Destroy(this.gameObject);
    }

    public override void inBuca()
    {
        GameManager.getManager().pallaInBuca(this);
        remove();
    }


    public bool isMoving() {
        if (rb.velocity != Vector3.zero) return true;
        else return false;
    }

    public void resetBall() {
        rb.velocity=Vector3.zero;
        rb.position = startPos;
    }
    
}
