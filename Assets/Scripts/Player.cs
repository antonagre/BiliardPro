using System;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public bool spezzate;
    public int n;
    public List<int> balls;

    public Player(int numero)
    {
        balls=new List<int>();
        this.n = numero;
    }
    
    public Player(int numero,bool spezzate) {
        balls=new List<int>();
        n = numero;
        this.spezzate = spezzate;
        initBalls();
    }

    public String getLabel() {
        String label="P"+n.ToString()+"--";
        foreach (int x in balls){
            label+=" "+x;
        }

        label += "(" + balls.Capacity.ToString() + ")";

        return label;
    }


    private void initBalls()
    {
        if (spezzate) {
            for (int i = 9; i < 16; i++)
            {
                balls.Add(i);
            }
        }
        else
        {
            for (int i = 1; i < 8; i++)
            {
                balls.Add(i);
            }
        }
    }

    public bool remove(int b) {
        balls.Remove(b);
        if (balls.Count == 0) return true;
        else return false;
    }
    
}