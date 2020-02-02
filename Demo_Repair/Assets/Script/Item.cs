using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
       public ToolType ToolType;


       public int Consume;
       public bool Break;

       public ItemManager ItemManager;


       private void Start()
       {
              //Consume = 10;
              Break = false;
       }

       public void UseTool()
       {
              Consume--;

              if (Consume <= 0)
              {
                     if (Break) return;                   
                     Break = true;
                  
              }
       }
       public void Settype(ItemManager itemmanager)
       {
              this.ItemManager = itemmanager;
              ToolType = ToolType.Repair;
       }


}
