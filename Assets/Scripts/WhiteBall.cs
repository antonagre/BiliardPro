using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : Ball
{
    protected Rigidbody body;
    public float multiplier=1f;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        body = GetComponent<Rigidbody>();
    }

    public void Colpisci(Vector3 direction,float force) {
        body.velocity = direction * force*multiplier;
    }

    public override void inBuca() {
        Debug.Log("RIPRISTINO POSIZIONE ORIGINALE");
        body.position = startPos;
        body.angularVelocity=Vector3.zero;
        body.velocity = Vector3.zero;
    }
    void Update() {
        if(transform.position.y<0) inBuca();
    }
}
