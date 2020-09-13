using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour, IPointerClickHandler {
 
    private Transform battente;
    private bool isOpen=false;
    private Animator anim;
    public Camera mapCamera;
    private RectTransform _screenRectTransform;
 
    private void Awake() {
        anim = GetComponent<Animator>();
        _screenRectTransform = GetComponent<RectTransform>();
        battente = SteccaManager.getInstance().battente;
    }
 
    public void OnPointerClick(PointerEventData eventData) {
        if (isOpen) {
            SteccaManager.getInstance().enabled=true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_screenRectTransform, eventData.position, null, out Vector2 localClick);
            localClick.y = (_screenRectTransform.rect.yMin * -1) - (localClick.y * -1);
            Vector2 viewportClick = new Vector2(localClick.x / _screenRectTransform.rect.xMax, localClick.y / (_screenRectTransform.rect.yMax ));
            Ray ray = mapCamera.ViewportPointToRay(new Vector3(viewportClick.x,viewportClick.y, 0));
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                battente.position = hit.point;
            }
        } else {
            SteccaManager.getInstance().enabled=false;
        }
        anim.SetTrigger("Operate");
        isOpen = !isOpen;        
    }
}