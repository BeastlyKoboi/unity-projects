using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardConditions
{
    // Postive Conditions 
    public static string Stoic => "Stoic";
    public static string Tenacious => "Tenacious";
    public static string Guarded => "Guarded";
    public static string Invincible => "Invincible";
    public static string Revenant => "Revenant";
    public static string Adaptable => "Adaptable";
    public static string Ambitious => "Ambitious";
    public static string Thorns => "Thorns";

    // Negative Conditions 
    public static string Poisoned => "Poisoned";
    public static string Bleeding => "Bleeding";
    public static string Fridged => "Fridged";
    public static string Reboot => "Reboot";
    public static string Helpless => "Helpless";

    // 
    private static readonly Dictionary<string, Action<CardModel, int>> conditionsDict = new Dictionary<string, Action<CardModel, int>>();

    // Method to add default actions to the dictionary
    public static void AddDefaultConditions()
    {
        AddCondition(Stoic, (CardModel card, int value) =>
        {
            Console.WriteLine("Default Action 1 executed.");
        });
        AddCondition(Tenacious, (CardModel card, int value) =>
        {
            Console.WriteLine("Default Action 2 executed.");
        });
    }

    public static void AddCondition(string key, Action<CardModel, int> action)
    {
        if (!conditionsDict.ContainsKey(key))
            conditionsDict.Add(key, action);
        else
            Debug.Log($"Key '{key}' already exists in the dictionary.");
    }

    public static void TriggerCondition(string key, CardModel card, int value)
    {
        if (conditionsDict.ContainsKey(key))
            conditionsDict[key](card, value);
    }
}

