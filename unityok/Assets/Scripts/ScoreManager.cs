using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    [Header("UI Reference")]
    public TMP_Text scoreText; // Reference to your TextMeshPro UI element
    
    private int currentScore = 0;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }

    // Call this when starting to set initial score
    public void InitializeScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
    }
}