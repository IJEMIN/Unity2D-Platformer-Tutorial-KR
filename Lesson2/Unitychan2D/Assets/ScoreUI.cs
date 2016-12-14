using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    private Text m_text;

    void Start()
    {
        m_text = GetComponent<Text>();
    }
	
	void Update ()
    {
        m_text.text = "점수: " + ScoreManager.GetInstance().score;	
	}
}
