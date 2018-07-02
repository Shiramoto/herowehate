using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Movimentação do avatar.
/// </summary>
public class PlayerMovement : MonoBehaviour {
	/// <summary>
	/// Velocidade do avatar (caminhando).
	/// </summary>
	public float playerSpeed = 12f;
	/// <summary>
	/// Velocidade do avatar (esquivando).
	/// </summary>
	public float playerDodgeSpeed = 20f;

    public float dodgeDuration = 0.5f;
	/// <summary>
	/// Zona neutra de um joystick onde o input não é computado.
	/// (Colocar no script PlayerControls)
	/// </summary>
	private float joystickTolerance = 0.1f;

	/// <summary>
	/// Indica o a direção ao qual o avatar está olhando.
	/// </summary>
	public Vector2 playerDirection = Vector2.zero;
    private Vector2 lastPlayerDirection = Vector2.zero;

	/// <summary>
	/// Componentes X e Y de movimento do personagem.
	/// </summary>
	public Vector2 playerMovement;

	/// <summary>
	/// Conta o tempo entre o apertar duplo do botão de esquiva.
	/// </summary>
	public float playerEvadeInputCounter;

    public string evadeButton = "Fire3";


	/// <summary>
	/// True para ativar a animação de caminhando.
	/// </summary>
	private bool isWalking = false;
    //private bool isStopped = false;
    private bool beginDodge = false;
    private bool isDodging = false;
    private float dodgeBeginTime = 0f;
    //private bool isParrying = false;

	private Animator playerAnimator;
    //private Rigidbody2D playerRigidbody;

	void Start () {
		playerAnimator = GetComponent<Animator> ();
        playerDirection.Set(0, -1);
        //playerRigidbody = GetComponent<Rigidbody2D>();
	}
	

	void Update ()
	{
        isWalking = false;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        
        if (isDodging)
        {
            if (Time.time > dodgeBeginTime + dodgeDuration)
            {
                isDodging = false;
                playerDirection.Set(0, 0);
            }
            else
            {
                playerDirection = lastPlayerDirection.normalized * 2;
                playerMovement = lastPlayerDirection.normalized * playerDodgeSpeed * Time.deltaTime;
                //playerRigidbody.transform.Translate(playerMovement.x, playerMovement.y, 0);
                transform.Translate(playerMovement.x, playerMovement.y, 0);
            }
        }
        if (!isDodging)
        {
            if (Input.GetButtonDown(evadeButton))
            {
                beginDodge = true;
            }
            //isStopped = true;

            if (!playerDirection.Equals(Vector2.zero))
            {
                lastPlayerDirection = playerDirection;
                playerDirection.Set(0, 0);
            }

            if ((Mathf.Abs(inputX)) > joystickTolerance)
            { 
                playerDirection.x = inputX;
                isWalking = true;
                //isStopped = false;
            }
            if (Mathf.Abs(inputY) > joystickTolerance)
            {
                playerDirection.y = inputY;
                isWalking = true;
                //isStopped = false;

            }
            if (beginDodge)
            {
                if (isWalking)
                {
                    playerDirection = playerDirection * 2;
                }
                else
                {

                }
                beginDodge = false;
                isDodging = true;
                isWalking = false;
                dodgeBeginTime = Time.time;
            }
        }

		playerAnimator.SetFloat ("SpeedX", playerDirection.x);
		playerAnimator.SetFloat ("SpeedY", playerDirection.y);
		playerAnimator.SetBool ("isWalking", isWalking);

		if (isWalking) {
            playerMovement = playerDirection.normalized * playerSpeed * Time.deltaTime;
            //playerRigidbody.transform.Translate(playerMovement.x, playerMovement.y, 0);
            transform.Translate (playerMovement.x, playerMovement.y, 0);
        }
        else{
			playerMovement.Set (0, 0);
		}
    }
}
