using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
// Implementation mostly draws from this source.
// In the future, I imagine each scene will have their own scene loader. 
// And when the scene loader is passed to the next scene, it destroys itself. 
// https://gamedevbeginner.com/how-to-load-a-new-scene-in-unity-with-a-loading-screen/#load_vs_load_async
public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public CanvasGroup canvasGroup;
    public string sceneToLoad;
    public UnityEvent fadeFinished;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 0.2f));
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!load.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(FadeLoadingScreen(0, 0.2f));
        loadingScreen.SetActive(false);
        if (fadeFinished != null)
            fadeFinished.Invoke();
        Destroy(gameObject);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
