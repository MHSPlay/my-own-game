using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    private bool menuVisible = false;

    public Dictionary<string, int> economyStats = new Dictionary<string, int>()
    {
        { "Wood requirement", 100 },
        { "Coal requirement", 50 },
        { "Food requirement", 75 }
    };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            menuVisible = !menuVisible;
            Cursor.visible = menuVisible;
            Cursor.lockState = menuVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    void OnGUI()
    {
        if (!menuVisible)
            return;

        // Text style settings
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 14;
        labelStyle.normal.textColor = Color.white;
        labelStyle.wordWrap = true;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 12;

        GUILayout.BeginArea(new Rect(10, 10, 300, 400), GUI.skin.box);
        {
            GUILayout.Label("Debug:", labelStyle);

            if (GUILayout.Button("Add 100 Wood", buttonStyle))
            {
                UpdateEconomyStat("Wood requirement", 100);
            }

            if (GUILayout.Button("Add 50 Coal", buttonStyle))
            {
                UpdateEconomyStat("Coal requirement", 50);
            }

            if (GUILayout.Button("Add 75 Food", buttonStyle))
            {
                UpdateEconomyStat("Food requirement", 75);
            }

            GUILayout.Space(10);

        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(320, 10, 300, 400), GUI.skin.box);
        {
            GUILayout.Label("The Economy:", labelStyle);

            foreach (var stat in economyStats)
            {
                GUILayout.Label("- " + stat.Key + ": " + stat.Value, labelStyle);
            }

            GUILayout.Space(10);

            GUILayout.Label("-------------------", labelStyle);

            if (GUILayout.Button("Reset Economy Stats", buttonStyle))
            {
                ResetEconomyStats();
            }
        }
        GUILayout.EndArea();
    }

    private void UpdateEconomyStat(string statName, int value)
    {
        if (economyStats.ContainsKey(statName))
        {
            economyStats[statName] += value;
        }
    }

    private void ResetEconomyStats()
    {
        foreach (var key in economyStats.Keys)
        {
            economyStats[key] = 0;
        }
    }

}
