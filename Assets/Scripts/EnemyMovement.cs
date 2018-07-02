using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float enemySpeed = 12f;
    public Vector2 enemyDirection = Vector2.zero;
    public Transform target;

    public Vector2 enemyMovement;


    private bool isWalking = true;

    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyDirection.Set(0, -1);
    }



    // Update is called once per frame
    void Update () {
        //transform.LookAt(target);
        enemyDirection.x = target.position.x - transform.position.x;
        enemyDirection.y = target.position.y - transform.position.y;
        enemyDirection.Normalize();
        transform.Translate(enemyDirection * enemySpeed * Time.deltaTime);
        //Vector3.MoveTowards(transform.position, target.position);

        enemyAnimator.SetBool("isWalking", isWalking);
        enemyAnimator.SetFloat("SpeedX", enemyDirection.x);
        enemyAnimator.SetFloat("SpeedY", enemyDirection.y);
    }
}
