using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Load the Gameplay
    public void LoadGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
