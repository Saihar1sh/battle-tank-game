using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI text;

    private int score = 0;

    public int scoreIncrement = 10;

    [SerializeField]
    private Image waveStartImage, AcheivementImage, PauseImage, PickupImage;

    [SerializeField]
    private Button pauseBtn, saveBtn, loadBtn, cheatModeBtn, startMenuBtn, exitBtn;

    [SerializeField]
    private TextMeshProUGUI AcheivementTxt, AcheivementTitle, WaveTxt, WaveNoTxt, pickupLines;

    private bool ImageUIVisible = true, waveStart, pauseMenuEnable = true;

    private Player player;
    private TankView tankView;

    private bool playerGodMode = false;

    void Start()
    {

        AcheivementImage.gameObject.SetActive(false);
        waveStartImage.gameObject.SetActive(false);
        PauseImage.gameObject.SetActive(false);
        PickupImage.gameObject.SetActive(false);

        pauseBtn.onClick.AddListener(PauseMenuEnable);
        saveBtn.onClick.AddListener(SaveGame);
        loadBtn.onClick.AddListener(LoadGame);
        startMenuBtn.onClick.AddListener(LoadStartMenu);
        exitBtn.onClick.AddListener(ExitGame);

        cheatModeBtn.onClick.AddListener(CheatMode);

        ServiceEvents.Instance.OnEnemyDeath += ScoreIncreament;
        ServiceEvents.Instance.OnEnemiesDestroyed += EnemiesDestroyedAchievements;
        ServiceEvents.Instance.OnBulletsFired += BulletsFiredAchievement;
        ServiceEvents.Instance.OnPlayersDestroyed += PlayersDestroyedAchievement;

        WaveNoTxt.text = "Wave No. " + 1;


    }

    public void InstantiatePlayerRef(GameObject playerGO)
    {
        tankView = playerGO.GetComponent<TankView>();
        player = playerGO.GetComponent<Player>();
    }

    private void Update()
    {
        waveStart = TankService.Instance.waveStarted;
        WaveStarts();

        if (Input.GetKeyDown(KeyCode.P))
            PauseMenuEnable();
    }

    private void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    private void BulletsFiredAchievement()
    {
        AcheivementTitle.text = "Si vis pacem, Para bellum";
        AcheivementTxt.text = "Bullets Fired: " + ServiceEvents.Instance.bulletsFiredAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage, 5f));
    }

    private void PlayersDestroyedAchievement()
    {
        AcheivementTitle.text = "Death is not the End";
        AcheivementTxt.text = "Player Tanks Destroyed: " + ServiceEvents.Instance.playersDeadAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage, 5f));

    }

    private void EnemiesDestroyedAchievements()
    {
        AcheivementTitle.text = "Uncle Sam brought home gifts";
        AcheivementTxt.text = "Enemies Destroyed: " + ServiceEvents.Instance.enemiesDeadAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage, 5f));
    }
    private void PauseMenuEnable()
    {
        PauseImage.gameObject.SetActive(pauseMenuEnable);
        pauseMenuEnable = !pauseMenuEnable;
        int timescale = pauseMenuEnable ? 1 : 0;
        Time.timeScale = timescale;
    }
    private void RampagePickup()
    {

        AcheivementTitle.text = "You are fired!!!!";
        AcheivementTxt.text = "Rampages picked : ";
    }
    private void RapidAmmoPickup()
    {
        AcheivementTitle.text = "Hell Fire!!!!!!";
        AcheivementTxt.text = "Rapid Ammos picked : ";

    }

    private void SaveGame()
    {
        player.SaveGame();
    }
    private void LoadGame()
    {
        player.LoadGame();
    }
    private void WaveStarts()
    {
        if (waveStart)
        {
            WaveTxt.text = "Wave " + TankService.Instance.waves + " Completed.";
            WaveNoTxt.text = "Wave No. " + TankService.Instance.waves;
        }
        waveStartImage.gameObject.SetActive(waveStart);
    }

    private void ScoreIncreament()
    {
        score += scoreIncrement;
        text.text = "" + score;
    }

    public void RampageMode()
    {
        pickupLines.text = "Rampage Ammo Picked";
        PickupImage.color = new Vector4(1, 0, 0, 0.5f);                     //red
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(PickupImage, 3f));

    }

    public void FireAmmoMode()
    {
        pickupLines.text = "Fire Ammo Picked";
        PickupImage.color = new Vector4(1, .5f, 0, 0.5f);                   //orange
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(PickupImage, 3f));

    }
    public void RapidAmmoMode()
    {
        pickupLines.text = "Rapid Ammo Picked";
        PickupImage.color = new Vector4(1, .92f, 0.016f, 0.5f);            //yellow
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(PickupImage, 3f));
    }

    public void HealthMode()
    {
        pickupLines.text = "Health Picked";
        PickupImage.color = new Vector4(0, 1, 0, 0.5f);                   //green
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(PickupImage, 3f));
    }



    private void CheatMode()
    {
        playerGodMode = !playerGodMode;
        if (playerGodMode)
        {
            pickupLines.text = "God Mode Activated";
            PickupImage.color = new Vector4(0, .7f, 1, 0.5f);
            tankView.PlayerCheatModeActivate();
            if (ImageUIVisible)
                StartCoroutine(ImageUIVisiblePeriod(PickupImage, 3f));
        }
        else
        {
            pickupLines.text = "God Mode Disabled";
            PickupImage.color = new Vector4(1, 1, 1, 0.5f);
            tankView.PlayerCheatModeDisabled();
            if (ImageUIVisible)
                StartCoroutine(ImageUIVisiblePeriod(PickupImage, 1f));
        }

    }

    //Coroutines

    IEnumerator ImageUIVisiblePeriod(Image image, float timePeriod)
    {
        GameObject gameObject = image.gameObject;
        ImageUIVisible = false;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(timePeriod);
        gameObject.SetActive(false);
        ImageUIVisible = true;
    }

}