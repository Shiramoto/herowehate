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
	public float playerWalkSpd;
	/// <summary>
	/// Aceleração do avatar (caminhando).
	/// </summary>
	public float playerWalkAccel;
	/// <summary>
	/// Aceleração do avatar (dash).
	/// </summary>
	public float playerDashAccel;
	/// <summary>
	/// Desaceleração do avatar (caminhando/dash/dano).
	/// </summary>
	public float playerStopAccel;
	/// <summary>
	/// Zona neutra de um joystick onde o input não é computado.
	/// (Colocar no script PlayerControls)
	/// </summary>
	private float joystickTolerance = 0.1f;

	/// <summary>
	/// Indica o a direção ao qual o avatar está olhando.
	/// </summary>
	public Vector2 playerDirection;

	/// <summary>
	/// Componentes X e Y de movimento do personagem.
	/// </summary>
	public Vector2 playerMovement;

	/// <summary>
	/// Conta o tempo entre o apertar duplo do botão de esquiva.
	/// </summary>
	public float playerEvadeInputCounter;

	/// <summary>
	/// True para ativar a animação de caminhando.
	/// </summary>
	private bool isWalking = false;
	private bool isStopping = false;

	private Animator playerAnimator;


	void Start () {
		playerAnimator = GetComponent<Animator> ();
	}
	

	void Update ()
	{
		if (isWalking) {
			isStopping = true;
		}
		isWalking = false;

		float inputX = Input.GetAxisRaw ("Horizontal");
		float inputY = Input.GetAxisRaw ("Vertical");
		float playerSpeedDelta = playerWalkSpd * Time.deltaTime;

		playerDirection.Set (0, 0);

		if ((Mathf.Abs (inputX)) > joystickTolerance) {
			playerDirection.x = inputX;
			isWalking = true;
//			isStopping = false;
		}
		if (Mathf.Abs (inputY) > joystickTolerance) {
			playerDirection.y = inputY;
			isWalking = true;
//			isStopping = false;

		}
		playerAnimator.SetFloat ("SpeedX", playerDirection.x);
		playerAnimator.SetFloat ("SpeedY", playerDirection.y);
		playerAnimator.SetBool ("isWalking", isWalking);

		if (isWalking) {
			playerMovement = playerDirection;
			playerMovement.Normalize ();
			transform.Translate (playerMovement.x * playerSpeedDelta, playerMovement.y * playerSpeedDelta, 0);
			//playerDirection.Normalize();
			//transform.Translate (playerDirection.x * playerSpeedDelta, playerDirection.y * playerSpeedDelta, 0);
		}else{
			playerMovement.Set (0, 0);
		}
	}
}
