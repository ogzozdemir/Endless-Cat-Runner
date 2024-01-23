using UnityEngine;

public class SelfHide : MonoBehaviour
{
    private void Hide()
    {
        GameController.instance.ContinueMusic("Game Start Music");
        gameObject.SetActive(false);
    }
    
    private void Disable() => gameObject.SetActive(false);
}