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
              switch (playerType)
              {
                     case PlayerType.Repairer:
                            Debug.Log("Repairer");
                    
                            break;
                     case PlayerType.Saboteur:
                            other.transform.GetComponent<Player>().SetDaze();
           
                            break;

                        

              }

              this.gameObject.SetActive(false);
       }
}
