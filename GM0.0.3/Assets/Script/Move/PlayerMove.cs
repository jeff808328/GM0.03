using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CommonMove
{
    private PlayerState PlayerState;
    private PlayerAnimation PlayerAnimation;

    private float LastMoveAni;

    void Start()
    {
        InitValueSet();

        InitComponmentSet();

        PlayerInitComponentSet();
    }

    void Update()
    {
        if (PlayerState.MoveAble)
        {
            if (PlayerState.ActionLayerNow < 3)
            {
                if (Input.GetKeyDown(KeyCode.R) & PlayerState.RollAble)
                {
                    Debug.Log("Roll trigger");

                    StartCoroutine(Roll(LastMoveDirection, PlayerState.RollAniLength, CharacterData.MaxMoveSpeed * DashAdjust));

                    PlayerAnimation.Animator.SetTrigger("Dash");
                }
            }

            if (PlayerState.ActionLayerNow <= 1)
            {
                if (Input.GetKeyDown(KeyCode.Space) & PlayerState.JumpTime < PlayerState.MaxJumpTime)
                {
                    StartCoroutine(Jump());

                    PlayerAnimation.Animator.SetTrigger("Jump");
                }

                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
                {
                    Run(-1);

                }
                else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
                {
                    Run(1);
                }
                else
                {
                    Brake();
                }
            }
        }

        if (!PlayerState.GroundTouching)
        {
            PlayerState.ActionLayerNow = 1;
            GravityMax = -20;
            AddSpeedAdjust *= 0.5f;
        }
        else
        {
            AddSpeedAdjust = OriginAddSpeedAdjust;
            PlayerState.JumpTime = 0;
            PlayerState.Jumping = false;
            GravityMax = 0;
        }

        Gravity();

        FinalMoveSpeed = new Vector2(HorizonSpeed, VerticalSpeed);
        Rd.velocity = FinalMoveSpeed;
    }

    private void PlayerInitComponentSet()
    {
        CommonState = this.GetComponent<PlayerState>();
        PlayerState = this.GetComponent<PlayerState>();

        PlayerAnimation = this.GetComponent<PlayerAnimation>();

        LastMoveAni = Time.time;
    }



}
