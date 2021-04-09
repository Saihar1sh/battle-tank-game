using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoSingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI text;

    private int score = 0;

    public int scoreIncrement = 10;

    [SerializeField]
    private Image waveStartImage, AcheivementImage, PauseImage;

    [SerializeField]
    private Button pauseBtn, saveBtn, loadBtn;

    [SerializeField]
    private TextMeshProUGUI AcheivementTxt, AcheivementTitle, WaveTxt, WaveNoTxt;

    private bool ImageUIVisible = true, waveStart, pauseMenuEnable = true;

    [SerializeField]
    private Player player;

    void Start()
    {
        AcheivementImage.gameObject.SetActive(false);
        waveStartImage.gameObject.SetActive(false);
        PauseImage.gameObject.SetActive(false);
        pauseBtn.onClick.AddListener(PauseMenuEnable);

        saveBtn.onClick.AddListener(SaveGame);
        loadBtn.onClick.AddListener(LoadGame);


        ServiceEvents.Instance.OnEnemyDeath += ScoreIncreament;
        ServiceEvents.Instance.OnEnemiesDestroyed += EnemiesDestroyedAchievements;
        ServiceEvents.Instance.OnBulletsFired += BulletsFiredAchievement;
        ServiceEvents.Instance.OnPlayersDestroyed += PlayersDestroyedAchievement;

        WaveNoTxt.text = "Wave No. " + 1;
    }
    private void Update()
    {
        //AcheivementTxt.enabled = UIVisible;
        waveStart = TankService.Instance.waveStarted;
        WaveStarts();

        if (Input.GetKeyDown(KeyCode.P))
            PauseMenuEnable();
    }

    private void BulletsFiredAchievement()
    {
        AcheivementTitle.text = "Si vis pacem, Para bellum";
        AcheivementTxt.text = "Bullets Fired: " + ServiceEvents.Instance.bulletsFiredAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage));
    }

    private void PlayersDestroyedAchievement()
    {
        AcheivementTitle.text = "Death is not the End";
        AcheivementTxt.text = "Player Tanks Destroyed: " + ServiceEvents.Instance.playersDeadAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage));

    }

    private void EnemiesDestroyedAchievements()
    {
        AcheivementTitle.text = "Uncle Sam brought home gifts";
        AcheivementTxt.text = "Enemies Destroyed: " + ServiceEvents.Instance.enemiesDeadAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage));
    }
    private void PauseMenuEnable()
    {
        PauseImage.gameObject.SetActive(pauseMenuEnable);
        pauseMenuEnable = !pauseMenuEnable;
        int timescale = pauseMenuEnable ? 1 : 0;
        Time.timeScale = timescale;
    }

    private void SaveGame()
    {
        player.SaveGame();
    }
    private void LoadGame()
    {
        player.LoadGame();
    }
    private void RampagePickup()
    {
        AcheivementTitle.text = "Love the smell of napalm in the morning";
        AcheivementTxt.text = "Rampages picked : ";
    }
    private void RapidAmmoPickup()
    {

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
    IEnumerator ImageUIVisiblePeriod(Image image)
    {
        GameObject gameObject = image.gameObject;
        ImageUIVisible = false;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        ImageUIVisible = true;
    }

}