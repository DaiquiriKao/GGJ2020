using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollier : MonoBehaviour
{
       public PlayerType playerType;

       private void OnTriggerEnter(Collider other)
       {
              playerType = transform.parent.GetComponent<Player>().Type;

              Debug.Log(other.gameObject.name);
              Debug.Log(other.gameObject.tag);
              Debug.Log(playerType);

              switch (playerType)
              {
                     case PlayerType.Repairer:
                            if (other.gameObject.tag == "Object") { 
                                   Debug.Log("Repair : " + other.gameObject.name);

                            other.gameObject.transform.GetComponent<Hat>().LoadPlus();
                            }
                            break;
                     case PlayerType.Saboteur:
                            //if(other.gameObject.tag=="Player")
                            other.transform.GetComponent<Player>().SetDaze();
           
                            break;

                        

              }

              this.gameObject.SetActive(false);
       }
}
