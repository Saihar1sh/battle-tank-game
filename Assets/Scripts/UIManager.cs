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

    public TextMeshProUGUI AcheivementTxt;

    private bool UIVisible = true;

    void Start()
    {
        AcheivementTxt.enabled = false;
        ServiceEvents.Instance.OnEnemyDeath += ScoreIncreament;
        ServiceEvents.Instance.OnEnemiesDestroyed += EnemiesDestroyedAchievements;
        ServiceEvents.Instance.OnBulletsFired += BulletsFiredAchievement;
        ServiceEvents.Instance.OnPlayersDestroyed += PlayersDestroyedAchievement;
    }
    private void Update()
    {
        //AcheivementTxt.enabled = UIVisible;
    }

    private void BulletsFiredAchievement()
    {
        AcheivementTxt.text = "Bullets Fired: " + ServiceEvents.Instance.bulletsFiredAchievement;
        if(UIVisible)
            StartCoroutine(UIVisiblePeriod(AcheivementTxt));
    }

    private void PlayersDestroyedAchievement()
    {
        AcheivementTxt.text = "Players Destroyed: " + ServiceEvents.Instance.playersDeadAchievement;
        if (UIVisible)
            StartCoroutine(UIVisiblePeriod(AcheivementTxt));

    }

    private void EnemiesDestroyedAchievements()
    {
        AcheivementTxt.text = "Enemies Destroyed: "+ServiceEvents.Instance.enemiesDeadAchievement;
        if (UIVisible)
            StartCoroutine(UIVisiblePeriod(AcheivementTxt));


    }

    private void ScoreIncreament()
    {
        text.text = "Score : " + score;
        score += 10;
    }
    IEnumerator UIVisiblePeriod(TextMeshProUGUI proUGUI)
    {
        UIVisible = false;
        proUGUI.enabled = true;
        yield return new WaitForSeconds(5f);
        proUGUI.enabled = false;
        UIVisible = true;
    }
}