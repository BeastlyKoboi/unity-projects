using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Volume
    public Slider volumeSlider;

    // Key Bindings
    public Dropdown shootDropdown;

    // Collisions
    public Toggle hestiaEarthColToggle;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        shootDropdown.value = PlayerPrefs.GetInt("ShootKey", 0);
        hestiaEarthColToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("HestiaEarthCollides", 1));
    }

    public void SaveExit()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("ShootKey", shootDropdown.value);
        PlayerPrefs.SetInt("HestiaEarthCollides", hestiaEarthColToggle.isOn? 1 : 0);

        SceneManager.LoadScene("Menu");
    }

}
