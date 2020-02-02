using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopPageMain : MonoBehaviour
{
    public Animator anim;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(delegate { StartCoroutine(PlayAnim()); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAnim()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}
