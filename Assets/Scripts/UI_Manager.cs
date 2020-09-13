using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager:MonoBehaviour{
    private static UI_Manager INSTANCE;
    [SerializeField] GameObject canvas;
    private GameManager manager;
    private Text counter;

    public static UI_Manager Instance
    {
        get
        {
            if (INSTANCE == null) {
                GameObject go = new GameObject();
                INSTANCE = go.AddComponent<UI_Manager>();
            }
            return INSTANCE;
        }
    }

    void Start()
    {
        manager = GameManager.getManager();
        counter = canvas.GetComponentInChildren<Text>();
    }

    public void updateCounter(int n)
    {
        counter.text = "IN BUCA: " + n;
        Debug.Log("BUCA "+n);
    }
}
