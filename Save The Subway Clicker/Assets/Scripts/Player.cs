using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // 
    [SerializeField]
    float money;

    public TextMeshProUGUI moneyUI;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        moneyUI.text = string.Format("{0:C2}", money);
    }

    /// <summary>
    /// Method to add money to the player
    /// </summary>
    /// <param name="earnings"></param>
    public void AddMoney(float earnings)
    {
        money += earnings;
        moneyUI.text = string.Format("{0:C2}", money);
    }
}
