using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngineInternal; // Required when Using UI elements.

public class ControlManager : MonoBehaviour,IEndDragHandler {
    public Slider mainSlider;
    private SteccaManager mgr;
    private float lastValue;
    private bool tira = false;
    public float sensitivityHor = 9.0f;
    private static ControlManager INSTANCE;
    private TCPclient client;
    public bool isPlaing = true;
    public Dictionary<String, String> data=new Dictionary<string, string>();


    
    public static ControlManager getInstance()
    {
        if(INSTANCE==null){
            GameObject go = GameObject.Find("Slider");
            INSTANCE=go.GetComponent<ControlManager>();
        }
        return INSTANCE;
    }
    public void Start() {
        mainSlider.maxValue = 0.5f;
        mainSlider.minValue = 0;
        client = new TCPclient(this);
    }
    
    public void OnEndDrag(PointerEventData data) {
        Debug.Log("drag end, value:"+mainSlider.value.ToString());
        tira = true;
    }

    public void Update()
    {
        if (SteccaManager.getInstance().enabled) {
            if (isPlaing)
            {
                play();
            }
            else
            {
                if (data.Count > 3) {
                    SteccaManager.getInstance().Reset();
                    watch(data);
                }
            }
        }
    }

    public void play() {
        data.Clear();
        bool doSend = false;
        if (mainSlider.value != 0)
        {
            SteccaManager.getInstance().Drag(mainSlider.value);
            data.Add("drag",mainSlider.value.ToString());
            doSend = true;
        }else
        {
            data.Add("drag","0");
        }

        if (Input.GetMouseButtonDown(1))
        {
            SteccaManager.getInstance().Reset();
            data.Add("reset","1");
            doSend = true;
        }else
        {
            data.Add("reset","0");
        }

        if (Input.GetMouseButton(1))
        {
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            Vector3 dir = Quaternion.Euler(0, delta, 0) * Vector3.forward;
            SteccaManager.getInstance().Rotate(delta);
            //SteccaManager.getInstance().Rotate(dir);
            data.Add("rotation",delta.ToString());
            doSend = true;
        } else
        {
            data.Add("rotation","0");
        }


        if (tira) {
            SteccaManager.getInstance().Tira();
            lastValue=mainSlider.value;
            mainSlider.value = 0;
            tira = false;
            data.Add("tira","1");
            doSend = true;
        }
        else
        {
            data.Add("tira","0");

        }
        if(doSend) client.SendMessage(data);
    }

    public void dataUpdate(Dictionary<String, String> d) {
        if (!isPlaing) data = d;
    }
    
    public void watch(Dictionary<String,String> control) {
        control.TryGetValue("drag", out String drag);
        control.TryGetValue("reset", out String reset);
        control.TryGetValue("rotation", out String rotation);
        control.TryGetValue("tira", out String tiraSt);
        if (drag != "0")
        {
            SteccaManager.getInstance().Drag(float.Parse(drag));
        }

        if (reset == "1")
        {
            SteccaManager.getInstance().Reset();
        }

        if (rotation != "0")
        {
            float delta = float.Parse(rotation);
            SteccaManager.getInstance().Rotate(delta);

        }
        if (tiraSt =="1") {
            SteccaManager.getInstance().Tira();
            tira = false;
        }
        data.Clear();
    }

    public float getValue()
    {
        return lastValue;
    }
    
}
