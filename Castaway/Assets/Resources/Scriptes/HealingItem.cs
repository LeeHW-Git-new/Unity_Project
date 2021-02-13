using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class HealingItem : ItemEffect
{
    public int healPoint = 0;
   public override bool ExecuteRole()
    {
        Debug.Log("PlayerHP Add;" + healPoint);
        return true;
    }
}
