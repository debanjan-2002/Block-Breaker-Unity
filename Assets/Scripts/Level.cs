using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;   // Serialized for debugging purpose

    SceneLoader sceneloader;
    GameStatus gameStatus;
    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        gameStatus = FindObjectOfType<GameStatus>();
    }
    public void CountBlocks()
    {
        breakableBlocks++;
    }
    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
            int currIndex = sceneloader.CurrIndex() + 1;
            gameStatus.levelText.text = "Level " + currIndex.ToString();
        }
    }
}
