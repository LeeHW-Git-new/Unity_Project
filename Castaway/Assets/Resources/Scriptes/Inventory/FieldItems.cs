using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.efts = _item.efts;

        item.itemMeshFilter = GetComponent<MeshFilter>();
        item.itemMeshRenderer = GetComponent<MeshRenderer>();
        item.itemMeshFilter.sharedMesh = _item.itemMesh;
        item.itemMeshRenderer.material.mainTexture = _item.itemTexture;
    }

    public Item GetItem()
    {
        return item;
    }


    public void DestroyItem()
    {
        Destroy(gameObject);
    }

}
