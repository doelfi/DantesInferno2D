using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public void OnButtonPress()
    {
        GameStats.lifes = 3;
        GameStats.coins = 0;
        SceneManager.LoadScene(1);
    }
}