using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    private int playerHealth;
    public static HealthDisplay Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateHealthText();
    }

    public void UpdateHealth(int newHealth)
    {
        playerHealth = newHealth;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = playerHealth.ToString();
    }
}