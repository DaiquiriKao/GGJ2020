using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FakePlayer
{
       public KeyCode Up;
       public KeyCode Down;
       public KeyCode Left;
       public KeyCode Right;
       public KeyCode Atk;
       public KeyCode Miss;

       public int ID;

       public int Speed;
       public int RotateAngle;

       bool canAtk;

       public PlayerState State;
       public PlayerType Type;

       public Collider collider;

       public Collider Atk_collider;

       public Animator animator;

       public Animator[] animators;

       public Item item;

       public Vector3 point;

       //public SpriteRenderer PlayerSprite;
       //public Transform LookAtTarget;


       private void Start()
       {
              canAtk = true;
              item = null;
              collider = transform.GetComponent<BoxCollider>();
              animator = animators[0];
       }
       private void Update()
       {
              if (State == PlayerState.Invincible|| State ==PlayerState.Dizzy) return;

              if (Input.GetKeyUp(Up)) SetIdle();
              if (Input.GetKeyUp(Down)) SetIdle();
              if (Input.GetKeyUp(Left)) SetIdle();
              if (Input.GetKeyUp(Right)) SetIdle();

              if (Input.GetKey(Up)) MoveForward();
              if (Input.GetKey(Down)) MoveBack();
              if (Input.GetKey(Left)) TurnLeft();
              if (Input.GetKey(Right)) TurnRight();

              if (Input.GetKeyUp(Atk)) Attack();
              if (Input.GetKey(Miss)) Sidestep();

              //if(item != null)
              //if (item.Break == true)
              //{
              //       animator = animators[0];
              //       item = null;
              //}
       }

       private void OnCollisionEnter(Collision collision)
       {
              if (item != null) return;
              if (collision.gameObject.tag == "Item")
              {
                     Debug.Log(collision.gameObject.name);
                     Item item = collision.transform.GetComponent<Item>();
                     item.ItemManager.Generate(item.ToolType); 
                     item.ItemManager.Items.Remove(item.gameObject);
                     this.item = item;
                     UseItool(item);
              }
       }
       public void SetIdle()
       {
              animator.SetBool("Run", false);
       }
       protected override void MoveForward()
       {
              animator.SetBool("Run", true);
              transform.Translate(Vector3.forward * Speed * Time.deltaTime);
              point = Vector3.forward;
       }
       protected override void MoveBack()
       {
              animator.SetBool("Run", true);
              transform.Translate(Vector3.forward * -Speed * Time.deltaTime);
              point = Vector3.forward * -1;
       }
       protected override void TurnRight()
       {
              animator.SetBool("Run", true);
              transform.localScale = new Vector3(-1, 1, 1);
              transform.Translate((Vector3.right * Speed) * Time.deltaTime);
              point = Vector3.right;
       }
       protected override void TurnLeft()
       {
              animator.SetBool("Run", true);
              transform.localScale = new Vector3(1, 1, 1);
              transform.Translate((Vector3.right * -Speed) * Time.deltaTime);
              point = Vector3.right * -1;
       }
       public void Sidestep()
       {
              if (item != null) if (item.ToolType == ToolType.Destruction) return;
              State = PlayerState.Invincible;

              StartCoroutine(coSidestep(0.3f));
       }

       public IEnumerator coSidestep(float Sidesteptime)
       {
              float time = Time.time;
              while (!(Mathf.Abs(time - Time.time) > Sidesteptime))
              {
                     transform.Translate((point * Speed * 4) * Time.deltaTime);
                     yield return null;
              }


              State = PlayerState.Normal;
       }

       public void UseItool(Item item)
       {
              switch (item.ToolType)
              {
                     case ToolType.Repair:

                            animator.gameObject.SetActive(false);
                            animator = animators[1];
                            animator.gameObject.SetActive(true);
                            Type = PlayerType.Repairer;
                            Debug.Log(animator.gameObject.name);
                            break;

                     case ToolType.Destruction:

                            animator.gameObject.SetActive(false);
                            animator = animators[2];
                            animator.gameObject.SetActive(true);
                            Type = PlayerType.Saboteur;
                            Debug.Log(animator.gameObject.name);


                            break;

              }
              item.transform.SetParent(this.transform);
              item.transform.gameObject.SetActive(false);
       }

       public void Attack()
       {
              if (item == null)
              {
                     return;
              }
              else if (item.Break)
              {
                     animator.gameObject.SetActive(false);
                     animator = animators[0];
                     animator.gameObject.SetActive(true);
                     item.ItemManager.Items.Remove(item.gameObject);
                     Type = PlayerType.Repairer;
                     Destroy(item.gameObject);
                     item = null;
              }
              if (!canAtk) return;
              if (item != null)
              {
                     //Atk_collider.transform.GetComponent<AtkCollier>().SetType(Type);
                     Atk_collider.gameObject.SetActive(true);

                     canAtk = false;
                     StartCoroutine(coAtkCD(0.8f));

                     animator.SetTrigger("Attack");
                     item.UseTool();
              }
       }

       public IEnumerator coAtkCD(float CD)
       {
              yield return new WaitForSeconds(CD/2f);
    
              Atk_collider.gameObject.SetActive(false);

              yield return new WaitForSeconds(CD / 2f);
              canAtk = true;
       }
       public void SetDaze()
       {
              StartCoroutine(coDaze());
       }
       IEnumerator coDaze()
       {
              animator.SetBool("Daze", true);
              State = PlayerState.Dizzy;
              yield return new WaitForSeconds(3);
              animator.SetBool("Daze", false);
              State = PlayerState.Normal;
       }


}
