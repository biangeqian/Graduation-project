using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAction : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    Image image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color=new Color(255,255,255,0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color=new Color(255,255,255,0);
    }

    // Start is called before the first frame update
    void Awake() 
    {
        image=transform.GetComponent<Image>();
    }
    private void OnEnable() 
    {
        image.color=new Color(255,255,255,0);
    }
}
