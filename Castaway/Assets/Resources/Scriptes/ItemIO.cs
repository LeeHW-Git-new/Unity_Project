using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public sealed class ItemIO
{
   public static void SaveData()
    {
        List<GameObject> item = Inventory.Instance.AllSlot;

        XmlDocument xmlDoc = new XmlDocument();
        XmlElement xmlElement = xmlDoc.CreateElement("ItemDB");
        xmlDoc.AppendChild(xmlElement);

        int cnt = item.Count;

        for(int i = 0; i< cnt; i++)
        {
            Slot itemInfo = item[i].GetComponent<Slot>();

            if(!itemInfo.isSlots())
            {
                continue;
            }

            XmlElement elementSetting = xmlDoc.CreateElement("Item");

            elementSetting.SetAttribute("slotNumber", i.ToString());
            elementSetting.SetAttribute("Name", itemInfo.ItemReturn().name);
            elementSetting.SetAttribute("Count", itemInfo.slot.Count.ToString());
            elementSetting.SetAttribute("MaxCount", itemInfo.ItemReturn().maxCnt.ToString());
            xmlElement.AppendChild(elementSetting);
        }

        xmlDoc.Save(Application.dataPath + "/Save/InventoryData.xml");
    }


    public static List<GameObject> Load(List<GameObject> slotList)
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Save/InventoryData.xml"))
            return slotList;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.dataPath + "/Save/InventoryData.xml");
        XmlElement xmlElement = xmlDoc["ItmeDB"];

        foreach (XmlElement itemElement in xmlElement.ChildNodes)
        {
            Slot slot = slotList[System.Convert.ToInt32(itemElement.GetAttribute("SlotNumber"))].GetComponent<Slot>();

            Item item = new Item();
            string name = itemElement.GetAttribute("Name");
            int maxcnt = System.Convert.ToInt32(itemElement.GetAttribute("MaxCount"));
            item.Init(name, maxcnt);

            int cnt = System.Convert.ToInt32(itemElement.GetAttribute("Count"));
            for (int i = 0; i < cnt; i++)
                slot.AddItem(item);
        }

        return slotList;

    }

}
