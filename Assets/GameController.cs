﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class GameController : MonoBehaviour
{
    LevelFactory factory; 
    LevelManager manager;
    LevelGenerator lg;
    Output output;
    SidebarController sidebar;
    BackgroundController background; 
    bool winning = false; 
    private void CheckWin()
    {
        if (GlobalState.GameMode == stringLib.GAME_MODE_ON && GlobalState.level != null)
        {
            winning = true;
            for (int i = 0; i < 5; i++)
            {
                if (GlobalState.level.Tasks[i] != GlobalState.level.CompletedTasks[i])
                {
                    winning = false;
                }
            }
            if (winning)
            {
                StartCoroutine(Win()); 
            }
        }
    }
    //.................................>8.......................................
    //************************************************************************//
    // Method: public void Victory()
    // Description: Switch to the GAME_END state. When Update() is called, appropriate
    // action is taken there.
    //@TODO: level5 seems to be a magic level or something to signal the end of the game?
    //************************************************************************//
    public void Victory()
    {
        manager.SaveGame();
        GlobalState.IsPlaying = false;
        GlobalState.CurrentONLevel = "level5";
        GlobalState.GameState = stateLib.GAMESTATE_GAME_END;
        SceneManager.LoadScene("Credits");
    }

    IEnumerator Win()
    {
        yield return new WaitForSecondsRealtime(2.2f); 
        if (GlobalState.level.NextLevel != GlobalState.GameMode + "leveldata" + GlobalState.FilePath && winning)
        {
            manager.SaveGame();
            GlobalState.GameState = stateLib.GAMESTATE_LEVEL_WIN;
            winning = false;
            Debug.Log("Enumerator Win"); 
            SceneManager.LoadScene("Cinematic"); 
        }
        else
        {
            Victory(); 
        }
    }
    /*
    public void SetLevel(string file)
    {
        factory = new LevelFactory(file);
        GlobalState.level = factory.GetLevel();
        lg.BuildLevel(); 
    }
    */
    public void WarpLevel(string file, string line)
    {
        factory = new LevelFactory(file);
        GlobalState.level = factory.GetLevel();
        lg.BuildLevel();
        lg.WarpPlayer(line); 
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = new LevelManager(); 
        lg = GameObject.Find("CodeScreen").GetComponent<LevelGenerator>();
        Debug.Log(GlobalState.CurrentONLevel);
        factory = new LevelFactory(GlobalState.GameMode + "leveldata" + GlobalState.FilePath + GlobalState.CurrentONLevel);
        GlobalState.level = factory.GetLevel();

        output = GameObject.Find("OutputCanvas").transform.GetChild(0).gameObject.GetComponent<Output>();
        sidebar = GameObject.Find("Sidebar").GetComponent<SidebarController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalState.GameState == stateLib.GAMESTATE_IN_GAME)
            CheckWin(); 
    }
}
