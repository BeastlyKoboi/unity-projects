using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LocalStorageManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveDataToLocalStorage(string key, string value);

    [DllImport("__Internal")]
    private static extern string LoadDataFromLocalStorage(string key);

    [DllImport("__Internal")]
    private static extern void ClearLocalStorage();

    public void SaveData(string key, string value)
    {
        SaveDataToLocalStorage(key, value);
    }

    public string LoadData(string key)
    {
        return LoadDataFromLocalStorage(key);
    }

    public void ClearData()
    {
        ClearLocalStorage();
    }
}
