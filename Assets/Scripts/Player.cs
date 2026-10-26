using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float movementValue;
    private bool highAttack;
    private bool lowAttack;
    private Animator animator;
    private SpriteRenderer[] renderers;
    public bool attacking = false;
    public float speed = 3f;
    public float damageFlashTime = 0.5f;
    public EnemyType attackHeight = EnemyType.Normal;


    public TMP_Text scoreBox;
    public static int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("starting");
        animator = GetComponent<Animator>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreBox.text = "Score: " + score;
        attacking = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacking");
        if (!attacking)
        {
            transform.position += Vector3.right * movementValue * speed * Time.deltaTime;
        }
    }

    void OnMovement(InputValue value)
    {
        movementValue = value.Get<float>();
        animator.SetBool("IsWalking",Math.Abs(movementValue)>0.1f);
    }
    
    void OnHighAttack(InputValue value)
    {
        highAttack = value.isPressed;
        if (highAttack && !attacking)
        {
            animator.SetTrigger("IsHighAttack");
            attackHeight = EnemyType.High;
        }
        
    }

    void OnLowAttack(InputValue value)
    {
        lowAttack = value.isPressed;
        if (lowAttack && !attacking)
        {
            animator.SetTrigger("IsLowAttack");
            attackHeight = EnemyType.Low;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "door")
        {
            Debug.Log("player hit door, sending to level 2");
            SceneManager.LoadScene("Scenes/Level2");
        }else if(collision.gameObject.name == "door2")
        {
            Debug.Log("player hit door2, sending to MM");
            SceneManager.LoadScene("Scenes/MainMenu");
        }
        else
        {
            Debug.Log("Player hit enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Die();
            PlayerHit();
            
        }
        
    }

    public void IncreaseScore()
    {
        score++;
    }

    void PlayerHit()
    {
        StartCoroutine(FlashRed());
        Debug.Log("Player hit");
        score--;
    }

    //Claude helped me figure out how to do this VVVVVVV
    IEnumerator FlashRed()
    {
        foreach(SpriteRenderer r in renderers)
        {
            r.color = Color.red;
        }

        yield return new WaitForSeconds(damageFlashTime);

        foreach(SpriteRenderer r in renderers)
        {
            r.color = Color.white;
        }
    }
}
