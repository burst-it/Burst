using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBtnAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<Animation>().Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var anim = this.GetComponentInChildren<Animation>();
        this.GetComponentInChildren<Animation>().Stop();
        this.GetComponentInChildren<Outline>().effectDistance = new Vector2(2, -1);
    }
}
