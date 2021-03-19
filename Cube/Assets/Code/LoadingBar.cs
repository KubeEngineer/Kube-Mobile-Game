using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public Slider progressBar;
    public GameObject LoadingScreen;
    public Text yukleme_Text;
    
    public void loading(string scene)
    {
        
        StartCoroutine(startLoading(scene));
        
    }

    IEnumerator startLoading(string scene)
    {
        LoadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            float progres = (int)Mathf.Clamp01(async.progress);          
            
            progressBar.value = progres;
            yukleme_Text.text = progres * 100+"%";
            yield return null;
        }
    }


   

   
}
