using System;
using UnityEngine;
using System.Collections;
using Network;
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
    
    public static ControlManager getInstance()
    {
        if(INSTANCE==null){
            INSTANCE=new ControlManager();
        }
        return INSTANCE;
    }
    public void Start() {
        mainSlider.maxValue = 0.5f;
        mainSlider.minValue = 0;
    }
    
    public void OnEndDrag(PointerEventData data) {
        Debug.Log("drag end, value:"+mainSlider.value.ToString());
        TypeInferenceRuleAttribute=true
    }

    public void Update()
    {
        if (SteccaManager.getInstance().enabled) {
            if (mainSlider.value != 0)
            {
                SteccaManager.getInstance().Drag(mainSlider.value);
                NetworkControlAdapter.getInstance().data["drag"] = mainSlider.value.ToString();
            }else
            {
                NetworkControlAdapter.getInstance().data["drag"] = "0";
            }

            if (Input.GetMouseButtonDown(1))
            {
                SteccaManager.getInstance().Reset();
                NetworkControlAdapter.getInstance().data["reset"] = "1";
            }else
            {
                NetworkControlAdapter.getInstance().data["reset"] = "0";
            }

            if (Input.GetMouseButton(1))
            {
                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                SteccaManager.getInstance().Rotate(delta);
                NetworkControlAdapter.getInstance().data["rotation"] = delta.ToString();
            } else
            {
                NetworkControlAdapter.getInstance().data["rotation"] = "0";
            }


            if (tira) {
                SteccaManager.getInstance().Tira();
                lastValue=mainSlider.value;
                mainSlider.value = 0;
                tira = false;
                NetworkControlAdapter.getInstance().data["tira"] = "1";
            }
            else
            {
                NetworkControlAdapter.getInstance().data["tira"] = "0";

            }
        }
    }

    public float getValue()
    {
        return lastValue;
    }
    
}
