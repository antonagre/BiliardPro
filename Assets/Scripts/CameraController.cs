using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera camera;
    public Transform camAnchor;
    private Quaternion lastRotation;
    private Quaternion offsetAngle;
    private static CameraController INSTANCE=null;
    private Transform stecca;
    public static CameraController getInstance() {
        if(INSTANCE==null){
            GameObject go = GameObject.Find("MainCamera");
            INSTANCE=go.GetComponent<CameraController>();
        }
        return INSTANCE;
    }

    void Start(){
        camera = GetComponent<Camera>();
        stecca = GameObject.Find("Stecca").transform;
        offsetAngle = Quaternion.Euler(45, 0, 0);
    }

    public void updateCamera(Vector3 dir)
    {
        camera.transform.position = camAnchor.position;
        camera.transform.rotation = Quaternion.LookRotation(dir,Vector3.up)*offsetAngle;
    }
    
    public void updateCamera()
    {
        camera.transform.position = camAnchor.position;
        camera.transform.rotation = Quaternion.FromToRotation(Vector3.forward,stecca.forward)*offsetAngle;
    }


}
        