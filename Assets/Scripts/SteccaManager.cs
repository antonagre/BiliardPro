using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteccaManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform battente;
    private static SteccaManager INSTANCE=null;
    private float directionX = 0;
    private float load = 1f;
    private Vector3 start;
    private WhiteBall ball;
    private bool tira=false;
    protected Vector3 direction;
    private LineRenderer line;
    private CameraController camera;
    
    public static SteccaManager getInstance() {
        if(INSTANCE==null){
            GameObject go = GameObject.Find("Stecca");
            INSTANCE=go.GetComponent<SteccaManager>();
        }
        return INSTANCE;
    }
    
    // Start is called before the first frame update
    void Awake() {
            battente = GameObject.Find("Battente").transform;
            camera = CameraController.getInstance();
            ball = battente.GetComponent<WhiteBall>();
            line = GetComponent<LineRenderer>();
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
    }

    public void Drag(float force) {
        load = force/ 0.3f;
        transform.position = start - direction  * load;
    }

    public void DoAnimation() {
        if (transform.position != start) {
            transform.position=Vector3.Lerp(transform.position,start,10 *Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, start) < 0.1f) {
            tira = false;
            ball.Colpisci(direction,load); 
        }
    }

    public void Tira() {
        tira = true;
    }

    void DrawLine()
    {
        RaycastHit hit=new RaycastHit();
        Ray ray = new Ray(battente.position,direction);
        if (Physics.Raycast(ray,out hit))
        {
            line.SetPosition(0,battente.position);
            line.SetPosition(1,hit.point);
        }
    }

    public void Rotate(float delta)
    {
        transform.RotateAround(battente.position, Vector3.up, delta);
        direction = -(transform.position - battente.position).normalized;
        camera.updateCamera(direction);
        DrawLine();
    }

    public void Reset() {
        start = battente.position;
        transform.position = start+offset;
        transform.localRotation=Quaternion.Euler(-Vector3.forward);
        camera.updateCamera(-Vector3.forward);
        DrawLine();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (StdBall b in GameManager.getManager().balls) {
                b.resetBall();
            }
            Debug.Log("#reinizializzo mappa#");
        }
        if (tira) {
            DoAnimation();
        }
    }
}
