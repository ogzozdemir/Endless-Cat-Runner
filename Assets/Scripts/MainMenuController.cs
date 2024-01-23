using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform catModel;
    private float turnValue;

    private void Start() => turnValue = Random.Range(0, 360);
   
    void Update()
    {
        turnValue += 500f * Time.deltaTime;
        catModel.rotation = Quaternion.Euler(0, turnValue, 0);
        
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Game Scene");
    }
}
