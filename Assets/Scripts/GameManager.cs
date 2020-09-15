using System.Collections.Generic;
using UnityEngine;

public class GameManager{
    public Player p1;
    public Player currentPlayer;
    public Player p2;
    private static GameManager _instance=null;
    public List<StdBall> balls;
    private WhiteBall battente = SteccaManager.getInstance().battente.gameObject.GetComponent<WhiteBall>();
    public bool inBuca=false;
    public bool tiroCompleto;

    public static GameManager getManager() {
        if (_instance == null) {
            _instance=new GameManager();
        }
        return _instance;
    }
    
    private GameManager() {
        balls=new List<StdBall>();
        p1=new Player(1,true);
        p2=new Player(2,false);
        //set player p1 first
        currentPlayer = p1;
    }

    public bool checkBallsMoving() {
        if (battente.isMoving()) {
            return true;
        }
        foreach (StdBall b in balls) {
            if (b.isMoving()) {
                return true;
            }
        }
        return false;
    }

    public bool checkFirstHit() {
        if (currentPlayer.balls.Contains(battente.getFirstHit().ballNumber)) {
            return true;
        }

        return false;
    }

    public bool pallaInBuca(StdBall ball) {
        Debug.Log("player "+currentPlayer.n+" ha inbucato la palla n:"+ball.ballNumber);
        balls.Remove(ball);
        if (currentPlayer.balls.Contains(ball.ballNumber)) {
            currentPlayer.remove(ball.ballNumber);
            inBuca = true;
            return true;
        }
        else if(p1!=currentPlayer) {
            p1.remove(ball.ballNumber);
            return false;
        }
        else{
            p2.remove(ball.ballNumber);
            return false;
        }
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
