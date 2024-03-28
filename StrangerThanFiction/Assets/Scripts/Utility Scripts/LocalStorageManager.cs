using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LocalStorageManager : MonoBehaviour
{
#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void SaveDataToLocalStorage(string key, string value);

    [DllImport("__Internal")]
    private static extern string LoadDataFromLocalStorage(string key);

    [DllImport("__Internal")]
    private static extern void ClearLocalStorage();
#endif

    public void SaveData(string key, string value)
    {
#if UNITY_WEBGL
        SaveDataToLocalStorage(key, value);
#endif
    }

    public string LoadData(string key)
    {
#if UNITY_WEBGL
        return LoadDataFromLocalStorage(key);
#else
        return default;
#endif
    }

    public void ClearData()
    {
#if UNITY_WEBGL
        ClearLocalStorage();
#endif
    }
}
