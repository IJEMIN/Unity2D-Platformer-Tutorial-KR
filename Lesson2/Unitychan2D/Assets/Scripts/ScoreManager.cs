using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int score = 0;

    private static ScoreManager instance;

    public static ScoreManager GetInstance()
    {
        if(!instance)
        {
            instance = new GameObject("ScoreManager").AddComponent<ScoreManager>();
        }
        return instance;
    }
}
