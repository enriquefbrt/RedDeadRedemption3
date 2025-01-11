using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private VerticalLimit verticalLimit;

    private void Start() {
        healthManager.OnPlayerDeath += ToGameOverScreen;
        BossBehavior.OnBossDeath += ToWinScreen;
        verticalLimit.OnLimitReached += ToGameOverScreen;
    }

    private void ToGameOverScreen() {
        SceneManager.LoadScene("GameOver");
    }

    private void ToWinScreen() {
        StartCoroutine(WaitThenVictory());
    }

    IEnumerator WaitThenVictory()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinScreen");
    }
}
