using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Network
{
    public class NetworkControlAdapter :MonoBehaviour
    {
        public Dictionary<String, String> data;
        public float sensitivityHor = 9.0f;
        private bool isMaster = false;
        private static NetworkControlAdapter INSTANCE=null;

        
        public static NetworkControlAdapter getInstance()
        {
            if(INSTANCE==null){
                GameObject go = GameObject.Find("Stecca");
                INSTANCE = go.GetComponent<NetworkControlAdapter>();
            }
            return INSTANCE;
        }

        private NetworkControlAdapter() {
            data=new Dictionary<string, string>();
        }
        
        public void Update()
        {
            if (isMaster) {
                if (SteccaManager.getInstance().enabled)
                {
                    if (data["drag"] != "0")
                    {
                        SteccaManager.getInstance().Drag(float.Parse(data["drag"]));
                    }

                    if (data["reset"] == "1")
                    {
                        SteccaManager.getInstance().Reset();
                    }

                    if (data["rotation"] != "0")
                    {
                        float delta = float.Parse(data["rotation"]) * sensitivityHor;
                        SteccaManager.getInstance().Rotate(delta);

                    }
                    if (data["tira"] =="1") {
                        SteccaManager.getInstance().Tira();
                        lastValue=mainSlider.value;
                        mainSlider.value = 0;
                        tira = false;
                    }
                }
            }else
            {
                ControlManager.getInstance().enabled = true;
                

            }
        }
        
    }
}