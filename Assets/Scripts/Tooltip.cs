using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipText;

    private bool isPointerInside = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerInside)
        {
            isPointerInside = true;
            tooltipText.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerInside)
        {
            isPointerInside = false;
            tooltipText.SetActive(false);
        }
    }
}