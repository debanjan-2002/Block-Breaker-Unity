using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameStatus gamestatus;

    // State variables
    [SerializeField] int timesHit = 0;  // Serialized just for debug purposes
    private void Start()
    {
        gamestatus = FindObjectOfType<GameStatus>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
       
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Block sprite is missing from the array " + gameObject.name);
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
        gamestatus.AddToScore();
        level.BlockDestroyed();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        //Debug.Log(collision.gameObject.name); - > prints the object which hits the block (in this case ball)
        TriggerSparklesVFS();
    }
    private void TriggerSparklesVFS()
    {
        if (tag == "Breakable")
        {
            GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
            Destroy(sparkles, 1f);
        }

    }
}
