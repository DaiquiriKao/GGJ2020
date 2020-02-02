using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollier : MonoBehaviour
{
    public PlayerType playerType;
    private Player.OnGetScore onGetScore;

    public void SetOnGetScore(Player.OnGetScore action)
    {
        onGetScore = action;
    }
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
                            onGetScore(10);
                        }
                            break;
                     case PlayerType.Saboteur:
                            //if(other.gameObject.tag=="Player")
                            other.transform.GetComponent<Player>().SetDaze();
                            onGetScore(15);
           
                            break;

                        

              }

              this.gameObject.SetActive(false);
       }
}
