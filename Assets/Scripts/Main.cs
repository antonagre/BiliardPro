using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameManager mgr;
    
    void Start() {
        mgr=GameManager.getManager();
    }

    void Update(){
        if (mgr.tiroCompleto && !mgr.checkBallsMoving()) {
            if (mgr.checkFirstHit() && mgr.inBuca) {
                mgr.continuaTurno();
            }else {
                mgr.cambiaTurno();
            }
        }
    }
}
