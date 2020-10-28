﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Killholder : MonoBehaviour
{
    [SerializeField]
    Text text;

    public void Setup (string player, string source)
    {
        text.text = "<b>" + source + "</b>" + " killed " + "<b>" + player + "</b>";
    }
}