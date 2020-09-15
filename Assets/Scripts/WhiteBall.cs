using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : Ball
{
    public float multiplier = 1f;
    private StdBall firstHit;

    void Start()
    {
        init();
    }

    public void Colpisci(Vector3 direction, float force)
    {
        firstHit = null;
        rb.velocity = direction * force * multiplier;
    }

    public override void inBuca()
    {
        Debug.Log("RIPRISTINO POSIZIONE ORIGINALE");
        rb.position = startPos;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        if (transform.position.y < 0) inBuca();
    }

    public StdBall getFirstHit()
    {
        return firstHit;
    }

    public void OnTriggerEnter(Collider collider) {
        if (firstHit == null && collider.gameObject.tag=="Ball") {
            firstHit=collider.gameObject.GetComponent<StdBall>();
        }
    }
}
