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
    [SerializeField] private SlimeBehavior slimeBehavior;

    private void Start() {
        healthManager.OnPlayerDeath += ToGameOverScreen;
        slimeBehavior.OnDeath += FindBoss;
        verticalLimit.OnLimitReached += ToGameOverScreen;
    }

    private void ToGameOverScreen() {
        SceneManager.LoadScene("GameOver");
    }

    private void ToWinScreen() {
        StartCoroutine(WaitThenVictory());
    }

    private void FindBoss() {
        GameObject boss = GameObject.Find("Boss(Clone)");
        boss.GetComponent<BossBehavior>().OnBossDeath += ToWinScreen;
    }

    IEnumerator WaitThenVictory()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinScreen");
    }
}
