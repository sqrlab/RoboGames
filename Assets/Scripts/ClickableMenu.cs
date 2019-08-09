﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableMenu : MonoBehaviour
{
    public int index = -1; 
   void OnMouseDown(){
       OldMenu menu = GameObject.Find("Menu").GetComponent<OldMenu>(); 
       menu.onClick(index); 
   }

    public void onclickInput(){
        LeaderboardControl ld = GameObject.Find("Canvas").GetComponent<LeaderboardControl>(); 
        ld.onclickInput();
    }
}
