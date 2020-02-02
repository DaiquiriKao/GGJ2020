using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public  List<GameObject> Items;
       public int Count;
        public GameObject item;

       void Start()
    {
              Items = new List<GameObject>();
              for (int i = 0; i < Count; i++)
              {
                     Generate();
              }
              int B_Count = 0;
              while (B_Count < 3)
              {
                     int index = Random.Range(0, Items.Count);
                     if (Items[index].GetComponent<Item>().ToolType == ToolType.Repair)
                     {
                            Items[index].GetComponent<Item>().ToolType = ToolType.Destruction;
                            B_Count++;
                     }

              }
    }

       void Generate()
       {
              GameObject obj = Instantiate(item);
              obj.transform.GetComponent<Item>().Settype(this);
              int x = Random.Range(-40, 40);
              int z = Random.Range(-40, 40);
              obj.transform.position = new Vector3(x, 2, z);
              obj.transform.SetParent(this.transform);
              Items.Add(obj);
       }
}
