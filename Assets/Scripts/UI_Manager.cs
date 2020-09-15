using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager:MonoBehaviour{
    private static UI_Manager INSTANCE;
    [SerializeField] GameObject canvas;
    private GameManager manager;
    public Text p1_label;
    public Text p2_label;
    public Text cur_label;

    public static UI_Manager Get_instance() {
        return INSTANCE;
    }

    void Start()
    {
        manager = GameManager.getManager();
        INSTANCE = this;
    }

    public void Update(){
        p1_label.text = manager.p1.getLabel();
        p2_label.text = manager.p2.getLabel();
        cur_label.text = "(P"+manager.currentPlayer.n.ToString()+")";

    }
}
