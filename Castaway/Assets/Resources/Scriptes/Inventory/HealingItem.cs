using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class HealingItem : ItemEffect
{
    public float healPoint = 0;
   public override bool ExecuteRole()
    {
        GameManager.Instance.playerHP += healPoint;
        return true;
    }
}
