using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private GameObject table;
    private int currentTablePosition;
    private bool isAtTable = false;
    public bool isDragged=false;
    private CanvasGroup canvasGroup;

    private void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        isDragged=true;
        canvasGroup.alpha=.6f;
        canvasGroup.blocksRaycasts=false;
        if(table != null && isAtTable){
                table.GetComponent<Table>().RemovePersonFromTable(currentTablePosition);
                isAtTable = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position=Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        isDragged=false;
        canvasGroup.alpha=1f;
        canvasGroup.blocksRaycasts=true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down");
    }

    public int GetCurrentTablePosition(){
        return currentTablePosition;
    }

    public void SetCurrentTablePosition(int position){
        currentTablePosition = position;
    }

    public bool IsAtTable(){
        return isAtTable;
    }

    public void SetIsAtTable(bool sittingAtTable){
        isAtTable = sittingAtTable;
    }
}
