using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int health = 5;
    public Text gameOverMessage;
    private bool isOver = false;

    void Update()
    {
        if(isOver && Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }


    void OnDamage(int damageAmout)
    {
        if (health > 0)
        {
            health = health - damageAmout;
        }
        else
        {
            SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
            Debug.Log("죽은처리");
        }
    }

    void OnDeath()
    {
        GetComponent<Collider2D>().isTrigger = true;
        Invoke("TurnOff", 2f);
    }

    void TurnOff()
    {
        //gameObject.SetActive(false);
        isOver = true;
        gameOverMessage.text = "GAME OVER";
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        gameOverMessage.text = "";
    }
}
