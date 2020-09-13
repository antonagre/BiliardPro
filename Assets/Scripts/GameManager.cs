using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager{
    private Player p1;
    private Player currentPlayer;
    private Player p2;
    private static GameManager _instance;
    public List<StdBall> balls;
    public bool inBuca=false;
    public bool tiroCompleto=false;

    public static GameManager getManager() {
        if (_instance == null) {
            _instance=new GameManager();
        }
        return _instance;
    }
    
    private GameManager() {
        balls=new List<StdBall>();
        p1=new Player(1);
        p2=new Player(2);
        //set player p1 first
        currentPlayer = p1;
    }

    public bool checkBallsMoving() {
        foreach (StdBall b in balls) {
            if (b.isMoving()) return true;
        }
        return false;
    }

  

    public bool pallaInBuca(StdBall ball) {
        if (currentPlayer.balls.Contains(ball.ballNumber)) {
            currentPlayer.remove(ball.ballNumber);
            inBuca = true;
            Debug.Log("player "+currentPlayer.n+" ha vinto!!");
            return true;
        }
            Debug.Log("player "+currentPlayer.n+" ha inbucato una palla dell'avversario");
            return false;
    }

    public void continuaTurno() {
        SteccaManager.getInstance().Reset();
        inBuca = false;
        Debug.Log("current player P"+currentPlayer.n);
    }

    public void cambiaTurno() {
        if (currentPlayer == p1) currentPlayer = p2;
        else currentPlayer = p1;
        inBuca = false;
    }
    
}
