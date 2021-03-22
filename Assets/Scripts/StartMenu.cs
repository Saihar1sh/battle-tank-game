using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Button playBtn, settingBtn, exitBtn;

    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private Text progressText;

    void Start()
    {
        playBtn.onClick.AddListener(PlayGame);
        exitBtn.onClick.AddListener(QuitGame);

        loadingScreen.SetActive(false);
        progressSlider.value = progressSlider.minValue;
    }

    private void PlayGame()
    {
        StartCoroutine(LoadAsync(1));

    }
    private void QuitGame()
    {
        Debug.Log("Application Quitting");
    }

    IEnumerator LoadAsync(int sceneBuildIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
        loadingScreen.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            progressSlider.value = progress;
            progressText.text = progress * 100f + " %";
            yield return null;
        }
    }
}
