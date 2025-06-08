using UnityEngine;
using UnityEngine.EventSystems;



public class Pointer_Rules : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // detects if cursor hovers over a button/ object 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Click_Manager.Instance != null)
        {
            Click_Manager.Instance.isButtonHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Click_Manager.Instance != null)
        {
            Click_Manager.Instance.isButtonHovered = false;
        }
    }

}
