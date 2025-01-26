using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("WIN")]
    [SerializeField] GameObject playAgainBtn;
    [SerializeField] GameObject mainMenuBtn;
    [SerializeField] GameObject winContainerGO;
    [SerializeField] CanvasGroup winBackgroundCG;
    [SerializeField] TextMeshProUGUI playerXWinTxt;
    [SerializeField] TextMeshProUGUI countingRoundsTxt;
    [SerializeField] private int startSecondsRound = 30;

    [Space(5)]
    
    [Header("PAUSE")]
    [SerializeField] CanvasGroup pauseCG;
    [SerializeField] TextMeshProUGUI pauseTxt;

    [Space(5)]

    [Header("TIMER")]
    [Header("References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Transform timerContainer;
    [SerializeField] private Animator timerAnimator;
    [Header("variables")]
    [SerializeField] private int startMinutes = 1;
    [SerializeField] private int startSeconds = 30;

    private int player1Rounds = 0;
    private int player2Rounds = 0;
    private float remainingTime;
    private float remainingTimeforRound;
    private bool isTimerRunning = true;
    private bool showingWinPanel = true;
    private bool isFinalRound = true;
    private bool isPaused = false;

    [Space(5)]

    [Header("REFERENCES")]
    [SerializeField] private Transform transitionPanel;
    [SerializeField] private SpikeSpawner spikeSpawner;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        isTimerRunning = false;
        showingWinPanel = false;

        pauseCG.alpha = 0f;
        pauseCG.interactable = false;
        pauseCG.blocksRaycasts = false;

        transitionPanel.DOMoveX(-1920, 1f).SetEase(Ease.OutSine);
        remainingTime = (startMinutes * 60) + startSeconds;
        remainingTimeforRound = startSecondsRound;
        StartCoroutine(StartTimerWithDelay());
    }

    private IEnumerator StartTimerWithDelay()
    {
        timerText.text = "3";
        yield return new WaitForSeconds(1);
        timerText.text = "2";
        yield return new WaitForSeconds(1);
        timerText.text = "1";
        yield return new WaitForSeconds(1);
        timerText.text = "GO!";
        yield return new WaitForSeconds(1);

        isTimerRunning = true;

        UpdateTimerText();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isTimerRunning = false;
                spikeSpawner.StartSpikeRain();
                TextSuddenDeath();
            }

            UpdateTimerText();
        }

        if (showingWinPanel && !isFinalRound)
        {
            remainingTimeforRound -= Time.deltaTime;

            if (remainingTimeforRound <= 0)
            {
                remainingTimeforRound = 0;
                showingWinPanel = false;
                StartCoroutine(CloseWinPanel());
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (Input.GetKey(KeyCode.K))
        {
            ShowWinPanel("PLAYER 1");
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (isTimerRunning)
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
        else
        {
            timerText.text = "SUDDEN DEATH";
        }
    }

    private void TextSuddenDeath()
    {
        timerContainer.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1.5f);
        timerContainer.DOMoveY(980, 1.5f);
        timerText.fontSize = 90;
        timerAnimator.SetBool("IsSuddenDeath", true);
    }

    public void ResetForNextRound()
    {
        isTimerRunning = false;
        remainingTime = (startMinutes * 60) + startSeconds;

        timerContainer.DOScale(Vector3.one, 1.5f);
        timerContainer.DOMoveY(1020, 1.5f);
        timerAnimator.SetBool("IsSuddenDeath", false);

        UpdateTimerText();

        spikeSpawner.StopSpikeRain();
    }

    public void SetupFinalRound(string playerWhoWon)
    {
        isFinalRound = true;
        playerXWinTxt.text = "WINNER " + playerWhoWon;
        countingRoundsTxt.text = "";

        playAgainBtn.SetActive(true);
        mainMenuBtn.SetActive(true);
    }

    public void ShowWinPanel(string playerWhoWon)
    {
        if(playerWhoWon == "PLAYER 1")
        {
            player1Rounds++;
        }
        else if(playerWhoWon == "PLAYER 2")
        {
            player2Rounds++;
        }

        showingWinPanel = true;

        winContainerGO.SetActive(true);
        
        winBackgroundCG.DOFade(0.8f, 1f);
        winBackgroundCG.interactable = true;
        winBackgroundCG.blocksRaycasts = true;

        spikeSpawner.StopSpikeRain();
        if(player1Rounds >= 2 || player2Rounds >= 2)
        {
            SetupFinalRound(playerWhoWon);
        }
        else 
        {
            isFinalRound = false;

            playerXWinTxt.text = playerWhoWon + " WIN";
            countingRoundsTxt.text = player1Rounds.ToString() + " - " + player2Rounds.ToString();

            playAgainBtn.SetActive(false);
            mainMenuBtn.SetActive(false);  
        }
    }

    private IEnumerator CloseWinPanel()
    {
        showingWinPanel = false;

        winBackgroundCG.DOFade(0f, 1);
        winBackgroundCG.interactable = false;
        winBackgroundCG.blocksRaycasts = false;

        yield return new WaitForSeconds(1f);

        ResetForNextRound();
        StartCoroutine(StartTimerWithDelay());
        winContainerGO.SetActive(false);
    }

    public void TogglePause()
    {
        if (!isTimerRunning)
        {
            Debug.Log("Cannot pause during sudden death.");
            StartCoroutine(SuddenDeathText());
            return;
        }

        if (showingWinPanel)
        {
            Debug.Log("Cannot pause during win panel.");
            return;
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            Debug.Log("Game paused.");
            pauseCG.alpha = 0.8f;
            pauseCG.interactable = true;
            pauseCG.blocksRaycasts = true;
            pauseTxt.text = "PAUSE";
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("Game resumed.");
            pauseCG.alpha = 0f;
            pauseCG.interactable = false;
            pauseCG.blocksRaycasts = false;
            pauseTxt.text = "";
            Time.timeScale = 1f;
        }
    }

    private IEnumerator SuddenDeathText()
    {
        pauseTxt.text = "THERE IS NO SCAPE";
        yield return new WaitForSeconds(1.5f);
        pauseTxt.text = "";
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
