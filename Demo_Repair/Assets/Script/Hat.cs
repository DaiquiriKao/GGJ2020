using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hat : MonoBehaviour
{
       public int Pepairload_Max;
       public int Pepairload_Value;

       public Slider Loading;



       public void LoadPlus ()
       {
              Pepairload_Value++;

              float index = (float)Pepairload_Value / (float)Pepairload_Max;

              Loading.value = index;

       }

}
