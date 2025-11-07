using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySfx(6);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySfx(7);
    }


}
