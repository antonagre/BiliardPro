using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StdBall : Ball {
    public int ballNumber;

    void Start() {
        init();
        String name = gameObject.name;
        int i = name.IndexOf("_");
        ballNumber = Int32.Parse(name.Substring(i+1));
        GameManager.getManager().balls.Add(this);
    }
    
    public void remove() {
        Destroy(this.gameObject);
    }

    public override void inBuca()
    {
        GameManager.getManager().pallaInBuca(this);
        remove();
    }


    public void resetBall() {
        rb.velocity=Vector3.zero;
        rb.position = startPos;
    }
    
}
