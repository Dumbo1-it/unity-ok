using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public int scoreValue = 1; // Fixed to always give 1 point
    public float lifetime = 2f;
    public GameObject destroyEffect;
    
    private void OnMouseDown()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }
        
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    private void Start()
    {
        // Destroy after lifetime if not clicked
        Destroy(gameObject, lifetime);
    }
}