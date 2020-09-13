using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngineInternal; // Required when Using UI elements.

public class ControlManager : MonoBehaviour,IEndDragHandler {
    public Slider mainSlider;
    private SteccaManager mgr;
    private float lastValue;
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
        SteccaManager.getInstance().Tira();
        lastValue=mainSlider.value;
        mainSlider.value = 0;
    }

    public void Update()
    {
        if (SteccaManager.getInstance().enabled) {
            if (mainSlider.value != 0)
            {
                SteccaManager.getInstance().Drag(mainSlider.value);
            }

            if (Input.GetMouseButtonDown(1))
            {
                SteccaManager.getInstance().Reset();
            }

            if (Input.GetMouseButton(1))
            {
                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                SteccaManager.getInstance().Rotate(delta);

            }
        }
    }

    public float getValue()
    {
        return lastValue;
    }
    
}
