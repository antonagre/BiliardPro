using System.Collections.Generic;
using UnityEngine;

public class Player {
    public bool spezzate;
    public int n;
    public List<int> balls;

    public Player(int numero)
    {
        this.n = numero;
    }

    public void setSpezzate(bool spezzate)
    {
        this.spezzate = spezzate;
        initBalls();
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
            for (int i = 1; i < 9; i++)
            {
                balls.Add(i);
            }
        }
    }

    public bool remove(int b)
    {
        balls.Remove(b);
        if (balls.Count == 0) return true;
        else return false;
    }
    
}