using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour {
    /// <summary>
    /// Public references for the left and right wheels
    /// </summary>
    public Rigidbody lw, rw;

    public GameObject leftShoulder, rightShoulder;

    public float testTorque;

    public float brakeForce;

    public Text speedUI;
    
    private Rigidbody rb;

    private float moveDrag = 0;
    public float maxAngVel;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lw.maxAngularVelocity = maxAngVel;
        rw.maxAngularVelocity = maxAngVel;
    }

	// Update is called once per frame
	void Update () {
        // Check for touch controls
        TouchMove();

        // Check for keyboard controls for debbug and test
        KeyboardMove();

        CheckSpeed();        
    }


    /// <summary>
    /// Check for touch controls
    /// 
    /// Cycles trought all the touch inputs, 
    /// checks their change in y axis since the last frame 
    /// and applies this as a toque on the right or left wheel 
    /// depending on which side of the screen was the touch registered
    /// </summary>
    private void TouchMove()
    {
        foreach (Touch touch in Input.touches)
        {
            // Get the change in vertical position since last frame to drive the wheels
            Vector2 touchDeltaPosition = touch.deltaPosition;
            Vector3 tor = new Vector3(0f, touchDeltaPosition.y, 0f) * testTorque;

            // Get the position of the current touch for "animation" purposes
            Vector2 touchArmPosition = touch.position;
            Vector3 shoulderRotation =new Vector3(0, 40*(touchArmPosition.y - Screen.height / 2) / (Screen.height / 2));

            // Check on which side of the screen are we touching the phone to select the appropiate wheel
            if (touch.position.x < Screen.width / 2)
            {
                // Touch on the left

                // Move left arm to the left touch location
                leftShoulder.transform.localEulerAngles = shoulderRotation;

                // On still touch we brake
                if (touch.phase == TouchPhase.Stationary)
                {
                    BrakeLeft();
                }
                // On moved touch we accelerate
                else if (touch.phase == TouchPhase.Moved)
                {
                    MoveLeft(tor);
                }
                // On touch end we return arms to rest
                else if (touch.phase == TouchPhase.Ended)
                {
                    // TODO: Smooth the animation
                    leftShoulder.transform.localEulerAngles = new Vector3();
                }
            }
            else if (touch.position.x > Screen.width / 2)
            {
                // Touch on the right

                // Move right arm to the right touch location
                rightShoulder.transform.localEulerAngles = -shoulderRotation;

                if (touch.phase == TouchPhase.Stationary)
                {
                    BrakeRight();
                }
                // On moved touch we accelerate
                else if (touch.phase == TouchPhase.Moved)
                {
                    MoveRight(tor);
                }
                // On touch end we return arms to rest
                else if (touch.phase == TouchPhase.Ended)
                {
                    // TODO: Smooth the animation
                    rightShoulder.transform.localEulerAngles = new Vector3();
                }
            }
        }
    }

    /// <summary>
    /// Check for keyboard controls
    /// 
    /// Grabs inputs from the keyboard and applies 
    /// torque to the respective wheels, W and S for the 
    /// left wheel and the UP and DOWN arrow for the right wheel.
    /// </summary>
    private void KeyboardMove()
    {
        // Check for right wheel
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Right wheel forward
            Vector3 tor = Vector3.up * testTorque;
            MoveRight(tor);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // Right wheel backwards
            Vector3 tor = Vector3.down * testTorque;
            MoveRight(tor);
        }

        // Check for left wheel
        if (Input.GetKey(KeyCode.W))
        {
            // Left wheel forward
            
            Vector3 tor = Vector3.up * testTorque;
            MoveLeft(tor);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Left wheel backwards
            Vector3 tor = Vector3.down * testTorque;
            MoveLeft(tor);
        }
    }

    /// <summary>
    /// Applies a torque to the left wheel
    /// </summary>
    /// <param name="torque">Torque to be applied to the left wheel</param>
    private void MoveLeft(Vector3 torque)
    {
        lw.drag = moveDrag;
        lw.AddRelativeTorque(torque);
    }

    /// <summary>
    /// Applies a torque to the right wheel
    /// </summary>
    /// <param name="torque">Torque to be applied to the right wheel</param>
    private void MoveRight(Vector3 torque)
    {
        rw.drag = moveDrag;
        rw.AddRelativeTorque(torque);
    }

    private void BrakeLeft()
    {
        if(lw.angularVelocity != Vector3.zero)
        {
            lw.angularVelocity = Vector3.zero;
            lw.drag = brakeForce;
        }
    }

    private void BrakeRight()
    {
        if (rw.angularVelocity != Vector3.zero)
        {
            rw.angularVelocity = Vector3.zero;
            rw.drag = brakeForce;
        }
    }

    private void CheckSpeed()
    {
        float speed = Vector3.Magnitude(rb.velocity);
        speedUI.text = (speed>0) ? (speed*100).ToString().Substring(0,2) : "0";
    }

    public Vector3 GetSpeed()
    {
        return rb.velocity;
    }
}
