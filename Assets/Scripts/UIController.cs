using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }

    [Space(5), Header("References"), Space(15)]
    [SerializeField] private TMP_Text startText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text increaseScoreText;
    [SerializeField] private GameObject endGameLayer;
    [SerializeField] private GameObject happyCat;

    [Space(5), Header("Game Status"), Space(15)]
    private bool highscorePassed;

    private void Awake() => instance = this;
    private void Start()
    {
        endGameLayer.SetActive(false);
        highscoreText.text = "Highscore: " + Mathf.Round(GameController.instance.highscorePoints);
    }

    private void Update()
    {
        if (GameController.instance.runStarted && !GameController.instance.gameEnded)
        {
            scoreText.text = Mathf.Round(GameController.instance.scorePoints).ToString();

            if (!highscorePassed && Mathf.Round(GameController.instance.scorePoints) > GameController.instance.highscorePoints)
            {
                highscoreText.text = string.Empty;
                happyCat.SetActive(true);
                
                GameController.instance.PauseMusic();
                GameController.instance.PlayAudio("Happy Music");
                
                highscorePassed = true;
            }
        }
    }

    public void IncreaseScore(int score)
    {
        increaseScoreText.gameObject.SetActive(true);
        increaseScoreText.gameObject.GetComponent<Animation>().Play();
        increaseScoreText.gameObject.GetComponent<Animation>().Rewind();
        increaseScoreText.text = "+" + score;
    }

    public void EndGameScreen() => endGameLayer.SetActive(true);

    public IEnumerator GameCountdown()
    {
        ChangeStartText("READY");
        yield return new WaitForSecondsRealtime(.9f);
        ChangeStartText("SET");
        yield return new WaitForSecondsRealtime(.9f);
        ChangeStartText("GO");
        
        DisableStartText();
        EnableScoreText();
    }
    
    private void ChangeStartText(string text) => startText.text = text;
    
    private void DisableStartText() => startText.GetComponent<Animation>().Play();
    private void EnableScoreText()
    {
        scoreText.GetComponent<Animation>().Play();
        highscoreText.GetComponent<Animation>().Play();
    }
}
