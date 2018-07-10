using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 startingPosition = Vector3.zero;
    public float playerSpeed = 12f;
    public const string dodgeButton = "Dodge";
    public float dodgeSpeed = 20f;
    public float dodgeDuration = 0.5f;
    public const string attackButton = "Attack";
    public float attackDuration = 0.1f;
    public const string parryButton = "Parry";
    public float parryDuration = 0.5f;

    /// <summary>Conta o tempo entre o apertar duplo do botão de esquiva.</summary>
    [HideInInspector] public float dodgeInputCounter = 0f;

    /// <summary>Tolerância pra evitar valores muito pequenos.</summary>
    private float joystickTolerance = 0.1f;

    [HideInInspector] public Vector2 playerInput = Vector2.down;
    [HideInInspector] public Vector2 spriteDirection = Vector2.down;
    [HideInInspector] public Vector2 dodgeDirection = Vector2.down;
    [HideInInspector] public Vector2 playerMovement = Vector2.zero;
    //[HideInInspector] public Vector2 enemyAttack;

    [HideInInspector] public bool isWalking = false;
    [HideInInspector] public bool isInAction = false;
    [HideInInspector] public bool isDodging = false;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isParrying = false;
    [HideInInspector] public bool isLocked = false;
    [HideInInspector] public bool isHurt = false;

    private float actionEndTime = 0f;

    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        transform.position.Set(startingPosition.x, startingPosition.y, 0); //resetar posição
        transform.Translate(startingPosition.x - transform.position.x, startingPosition.y - transform.position.y, startingPosition.z - transform.position.z);

    }


    void Update()
    {
        if (isInAction)
        {
            UpdatePlayerAction();
        }
        if (!isInAction)
        {
            GetPlayerInput();
            if (!playerInput.Equals(Vector2.zero))
            {
                UpdateSpriteDirection();
                isWalking = true;
                playerMovement = playerInput.normalized * playerSpeed * Time.deltaTime;
            }
            else
            {
                isWalking = false;
                spriteDirection.Normalize();
                playerMovement = Vector2.zero;
            }
            switch (ButtonPressed())
            {
                case parryButton:
                    isWalking = false;
                    isParrying = true;
                    isInAction = true;
                    actionEndTime = Time.time + parryDuration;
                    playerMovement = Vector2.zero;
                    break;
                case dodgeButton:
                    isWalking = false;
                    isDodging = true;
                    isInAction = true;
                    if (playerInput.Equals(Vector2.zero))
                    {
                        dodgeDirection = spriteDirection.normalized;
                    }
                    else
                    {
                        dodgeDirection = playerInput.normalized;
                    }
                    spriteDirection = 3f * spriteDirection.normalized;
                    actionEndTime = Time.time + dodgeDuration;
                    playerMovement = dodgeDirection * dodgeSpeed * Time.deltaTime;
                    break;
                case attackButton:
                    isWalking = false;
                    isAttacking = true;
                    isInAction = true;
                    actionEndTime = Time.time + attackDuration;
                    playerMovement = Vector2.zero;
                    break;
                    //default:
            }
        }

        //Executa o movimento
        transform.Translate(playerMovement.x, playerMovement.y, 0);

        //define variaveis do Animator
        playerAnimator.SetFloat("SpeedX", spriteDirection.x);
        playerAnimator.SetFloat("SpeedY", spriteDirection.y);
        playerAnimator.SetBool("isWalking", isWalking);
        //playerAnimator.SetBool("isAttacking", isAttacking);
        //playerAnimator.SetBool("isParrying", isParrying);
        //playerAnimator.SetBool("isHurt", isHurt);

    }

    void UpdatePlayerAction()
    {
        if (actionEndTime < Time.time)
        {
            isInAction = false;
            if (isDodging)
            {
                spriteDirection.Normalize();
                dodgeDirection = spriteDirection;
                isDodging = false;
            }
            isAttacking = false;
            isParrying = false;
            isHurt = false;
        }
    }

    string ButtonPressed()
    {
        if (isLocked)
        {
            return "locked";
        }
        if (isHurt)
        {
            return "hurt";
        }
        if (Input.GetButtonDown(parryButton))
        {
            return parryButton;
        }
        if (Input.GetButtonDown(dodgeButton))
        {
            return dodgeButton;
        }
        if (Input.GetButtonDown(attackButton))
        {
            return attackButton;
        }
        return "";
    }

    void GetPlayerInput()
    {
        //primeiro se obtem a direção do input
        playerInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if ((Mathf.Abs(playerInput.x)) < joystickTolerance)
        {
            playerInput.x = 0f;
        }
        if (Mathf.Abs(playerInput.y) < joystickTolerance)
        {
            playerInput.y = 0f;
        }
    }
    void UpdateSpriteDirection()
    {
        if (!playerInput.Equals(Vector2.zero))
        {
            //muda a direção do sprite se necessário
            if (Mathf.Abs(Vector2.Angle(playerInput, spriteDirection)) > 45f)
            {
                //se o angulo da direção nova for grande, o sprite mudará de direção
                if (Mathf.Abs(playerInput.x) - Mathf.Abs(playerInput.y) > (2 * joystickTolerance))
                {
                    spriteDirection.x = 2f * Mathf.Sign(playerInput.x);
                    spriteDirection.y = 0f;
                }
                else
                {// if (Mathf.Abs(playerInput.y) - Mathf.Abs(playerInput.x) > (2 * joystickTolerance)){
                    spriteDirection.x = 0f;
                    spriteDirection.y = 2f * Mathf.Sign(playerInput.y);
                }
            }
            //já que há um input, seta a flag
            isWalking = true;
            playerMovement = playerInput.normalized * playerSpeed * Time.deltaTime;
        }
        else
        {
            //input é nulo, logo o player estará parado
            isWalking = false;
            playerMovement = Vector2.zero;
        }
    }
}
