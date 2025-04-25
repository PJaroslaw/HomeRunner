using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Menu;

    Rigidbody2D rigidbody;

    float movSpeed = 4f;
    float speedLimiter = .7f;
    float inputHorizontal;
    float inputVertical;
    bool inputEsc;
    bool canMove = true;

    bool menuEnabled = false;

    Animator animator;
    string currentState;
    const string PLAYER_IDLE = "player_idle";
    const string PLAYER_RIGHT = "player_right";
    const string PLAYER_DOWN = "player_down";
    const string PLAYER_LEFT = "player_left";
    const string PLAYER_UP = "player_up";

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputEsc = Input.GetButtonDown("Cancel");
        if (inputEsc && !menuEnabled)
        {
            Menu.SetActive(true);
            menuEnabled = true;
            canMove = false;
        }
        else if (inputEsc)
        {
            Menu.SetActive(false);
            menuEnabled = false;
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        if ((inputHorizontal != 0 || inputVertical != 0) && canMove)
        {
            if (inputVertical != 0 && inputHorizontal != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rigidbody.velocity = new Vector2(inputHorizontal * movSpeed, inputVertical * movSpeed);

            if (inputHorizontal > 0)
            {
                ChangeAnimationState(PLAYER_RIGHT);
            }
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(PLAYER_LEFT);
            }
            else if (inputVertical > 0)
            {
                ChangeAnimationState(PLAYER_UP);
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState(PLAYER_DOWN);
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
