using UnityEngine;

public class FoodController : MonoBehaviour
{
    public int pointsToGive;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Collect"))
            gameObject.SetActive(false);
    }
}
