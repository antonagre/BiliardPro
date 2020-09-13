using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera camera;
    private Quaternion lastRotation;
    private Quaternion offsetAngle;
    private static CameraController INSTANCE=null;

    public static CameraController getInstance() {
        if(INSTANCE==null){
            GameObject go = GameObject.Find("MainCamera");
            INSTANCE=go.GetComponent<CameraController>();
        }
        return INSTANCE;
    }

    void Start(){
        camera = GetComponent<Camera>();
        offsetAngle = Quaternion.Euler(45, 0, 0);
    }

    public void updateCamera(Vector3 dir)
    {
        camera.transform.position = GameObject.Find("cameraPoint").transform.position;
        camera.transform.rotation = Quaternion.FromToRotation(Vector3.forward,dir)*offsetAngle;
    }

}
        