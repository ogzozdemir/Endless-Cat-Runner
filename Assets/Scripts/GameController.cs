using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    
    [Space(5), Header("References"), Space(15)]
    [HideInInspector] public AudioSource audioSource;
    
    [Space(5), Header("Audio Clips"), Space(15)]
    [SerializeField] private AudioClip[] audioClips;

    [Space(5), Header("Game Status"), Space(15)]
    public float scorePoints;
    public float highscorePoints;
    [HideInInspector] public bool gameEnded;
    [HideInInspector] public bool runStarted;
    private float oldMusicTime;
    [SerializeField] private float waitDelay;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        
        if (!PlayerPrefs.HasKey("highscore")) PlayerPrefs.SetFloat("highscore", highscorePoints);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        PlayAudio("Game Start Music");
        highscorePoints = PlayerPrefs.GetFloat("highscore", highscorePoints);
    }

    private void Update()
    {
        if (runStarted && !gameEnded)
            scorePoints += 10f * Time.deltaTime;

        if (gameEnded)
        {
            waitDelay -= Time.deltaTime;
            
            if (Input.anyKeyDown && waitDelay <= 0f)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameEnded()
    {
        gameEnded = true;
        runStarted = false;
        UIController.instance.EndGameScreen();

        if (scorePoints > highscorePoints)
        {
            highscorePoints = scorePoints;
            PlayerPrefs.SetFloat("highscore", highscorePoints);
        }
        
        audioSource.Stop();
        PlayAudio("Game End Music");
    }

    public void PlayAudio(string name)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == name)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    public void PlayAudioOneShot(string name)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == name)
                audioSource.PlayOneShot(clip);
        }
    }

    public void PauseMusic() => oldMusicTime = audioSource.time;
    
    public void ContinueMusic(string musicName)
    {
        audioSource.Stop();
        PlayAudio(musicName);
        audioSource.time = oldMusicTime;
    }
}
