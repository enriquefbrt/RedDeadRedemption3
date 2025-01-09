using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        SetAlphaToCero(playButton);
        SetAlphaToCero(quitButton);
    }

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("mundo");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    void SetAlphaToCero(Button button)
    {
        var buttonImage = button.GetComponent<Image>();
        var buttonColor = buttonImage.color;
        buttonColor.a = 0;
        buttonImage.color = buttonColor;
    }
}
