﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Class for player scoreboard
public class PlayerScoreboardItem : MonoBehaviour
{
    [SerializeField]
    Text usernameText;

    [SerializeField]
    Text killsText;

    public void Setup (string username, int kills)
    {
        usernameText.text = username;
        killsText.text = kills.ToString();
    }

}
