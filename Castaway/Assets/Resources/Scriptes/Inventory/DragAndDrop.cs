using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
    , IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    public Image itemIcon;
    public DADContainer container;
    bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemIcon.sprite == null)
        {
            return;
        }

        container.gameObject.SetActive(true);
        container.image.sprite = itemIcon.sprite;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDragging)
        {
            container.transform.position = eventData.position;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if(isDragging)
        {
            if(container.image.sprite != null)
            {
                Debug.Log("ImageChange");
            }
            else
            {
                itemIcon.sprite = null;
            }
        }

        isDragging = false;
        container.image.sprite = null;
        container.gameObject.SetActive(false);
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if(container.image.sprite !=null)
        {
            Sprite tempSprite = itemIcon.sprite;
            itemIcon.sprite = container.image.sprite;
            container.image.sprite = tempSprite;
        }
        else
        {
            container.image.sprite = null;
        }
    }


}
