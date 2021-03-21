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

    public int score  = 10 ;

    [SerializeField]
    private Image waveStartImage, AcheivementImage, PauseImage;

    [SerializeField]
    private TextMeshProUGUI AcheivementTxt, AcheivementTitle, WaveTxt;

    private bool TextVisible = true, ImageUIVisible = true, waveStart, pauseMenuEnable = true;


    void Start()
    {
        AcheivementImage.gameObject.SetActive(false);
        waveStartImage.gameObject.SetActive(false);

        ServiceEvents.Instance.OnEnemyDeath += ScoreIncreament;
        ServiceEvents.Instance.OnEnemiesDestroyed += EnemiesDestroyedAchievements;
        ServiceEvents.Instance.OnBulletsFired += BulletsFiredAchievement;
        ServiceEvents.Instance.OnPlayersDestroyed += PlayersDestroyedAchievement;
    }
    private void Update()
    {
        //AcheivementTxt.enabled = UIVisible;
        waveStart = TankService.Instance.waveStarted;
        WaveStarts();
        PauseMenuEnable();
    }

    private void BulletsFiredAchievement()
    {
        AcheivementTitle.text = "Si vis pacem, Para bellum";
        AcheivementTxt.text = "Bullets Fired: " + ServiceEvents.Instance.bulletsFiredAchievement;
        if(ImageUIVisible)
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
        AcheivementTxt.text = "Enemies Destroyed: "+ServiceEvents.Instance.enemiesDeadAchievement;
        if (ImageUIVisible)
            StartCoroutine(ImageUIVisiblePeriod(AcheivementImage));
    }
    private void PauseMenuEnable()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseImage.gameObject.SetActive(pauseMenuEnable);
            pauseMenuEnable = !pauseMenuEnable;
            int timescale = pauseMenuEnable ? 1 : 0;
            Time.timeScale = timescale;
        }
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
            WaveTxt.text = "Wave " + TankService.Instance.GetCurrentWave() + " Completed.";
        }
        waveStartImage.gameObject.SetActive(waveStart);
    }

    private void ScoreIncreament()
    {
        text.text = "Score : " + score;
        score += 10;
    }
/*    IEnumerator TextVisiblePeriod(TextMeshProUGUI proUGUI)
    {
        TextVisible = false;
        proUGUI.enabled = true;
        yield return new WaitForSeconds(5f);
        proUGUI.enabled = false;
        TextVisible = true;
    }
*/    IEnumerator ImageUIVisiblePeriod(Image image)
    {
        GameObject gameObject = image.gameObject;
        ImageUIVisible = false;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        ImageUIVisible = true;
    }

}