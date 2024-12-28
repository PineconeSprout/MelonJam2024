using UnityEngine;
using UnityEngine.EventSystems;

public class Chair : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject table;
    [SerializeField] private int seatNumber;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag!=null){
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition=GetComponent<RectTransform>().anchoredPosition;
            if(table != null){
                table.GetComponent<Table>().AddPersonToTable(eventData.pointerDrag, seatNumber);
                eventData.pointerDrag.GetComponent<Draggable>().SetCurrentTablePosition(seatNumber);
                eventData.pointerDrag.GetComponent<Draggable>().SetIsAtTable(true);
            }
            Debug.Log(eventData.pointerDrag.ToString());
        }
    }
}
