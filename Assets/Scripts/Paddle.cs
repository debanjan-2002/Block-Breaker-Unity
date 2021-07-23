using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] Ball ball;

    [SerializeField] float screenWidthInUnits = 16f;
    /* Here we have set the size of the main camera as 6 in unity, which means it covers 6 unity units (from the centre)
     * in VERTICAL direction. So total number of units in VERTICAL direction = 6*2 = 12. Now we have set the aspect ratio
     * as 4:3, so the total number of units in the HORIZONTAL (x) direction becomes = (4 * 12) / 3 = 16.
     */

    // Cache references
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        // float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        // This brings the mouse position in Unity units.

        // Trying something of my own : to make sure that the paddle doesn't go out of screen.
        //if (mousePosInUnits < 1)
        //  mousePosInUnits = 1f;
        //else if (mousePosInUnits > 15)
        //  mousePosInUnits = 15f;
        // The above if conditions will prevent the paddle from moving out of the screen.
        //Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);

        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ball.hasStarted)
            GetComponent<AudioSource>().Play();
    }
    private float GetXPos()
    {
        if (gameStatus.IsAutoPlayEnabled())
        {
            if(ball.hasStarted)
                return ball.transform.position.x;
            else
                return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
