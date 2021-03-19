using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SahnelereYollama : MonoBehaviour {
    public GameObject loadingScreen;
    public Slider slider;
    public Text yukleme_Text;
    public void loadLevel(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            slider.value = progress;
            yukleme_Text.text = (int)(progress * 100) + "%";
            yield return null;
        }
    }


    private void Start()
    {
        if (PlayerPrefs.GetInt("IlkAcilis") == 0)
        {
            YeniBaslama.GetYeniBaslama().Start();
           
        }
        
    }
   
    public void ExitGame()
    {
        Application.Quit();
    }
}
