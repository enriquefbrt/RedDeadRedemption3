using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        SceneManager.LoadScene("WinScreen");
    }
}
