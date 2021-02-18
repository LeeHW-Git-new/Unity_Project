using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equpment,
    Consumables,
    Etc,
    Weapon
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public Mesh itemMesh;
    public Texture itemTexture;

    [HideInInspector]
    public int itemCnt;
    [HideInInspector]
    public MeshFilter itemMeshFilter;
    [HideInInspector]
    public MeshRenderer itemMeshRenderer;

    public List<ItemEffect> efts;
    public bool Use()
    {
        bool isUsed = false;
        foreach(ItemEffect eft in efts)
        {
            isUsed = eft.ExecuteRole();
        }

        return isUsed;
    }


}
