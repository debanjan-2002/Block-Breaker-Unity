using UnityEngine;

public class Ball : MonoBehaviour
{
    //configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 10f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomfactor = 0.2f;

    // Cache variables
    Rigidbody2D myRigidBody2D;

    //state
    Vector2 paddleToBallVector;
    public bool hasStarted = false;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        //Here we don't need to explicitly define an object for the ball, because this script is already attached to the ball
        // game object. So just writing tranform.position means that we are automatically referring to the tranform of the ball.

        myRigidBody2D = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush,yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        // This gives the current coordinates of the paddle;

        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomfactor),Random.Range(0f, randomfactor));
        if (hasStarted && collision.gameObject.name != "Paddle")
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
