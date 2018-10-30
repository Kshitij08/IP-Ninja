using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();


public class Player : Character {


    public Canvas trademarkCanvas;
    public Text trademarkText;
    public Button yesTrademark;
    public Button noTrademark;


    private string SceneName;

    Scene scene;

    public static int score;


    //----------------------------
    public GameObject player;

    public Text portionText;
    public Button fullHealth;
    public Button halfHealth;
    public Button dontRevive;

    public Canvas portionCanvas;

    //----------------------------

    private static Player instance;

    public event DeadEventHandler Dead;

    public static Player Instance
    {
        get
        {
            if(instance  == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float accelerationSpeed;

    private bool immortal = false;


    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

    private float direction;
    private bool move;
    private float btnHorizontal;

    public Rigidbody2D MyRigidbody { get; set; }


    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    public static float Timer;

    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                //OnDead();

                if(scene.name == "Level3" || scene.name == "Level4" || scene.name == "Level5" || scene.name == "Level6" || scene.name == "Level7" || scene.name == "Level8" || scene.name == "Level9" )
                {
                    portionCanvas.gameObject.SetActive(true);
                }

                //Debug.Log("I'm dead");

                else
                {
                    OnDead();
                    Debug.Log("OnDead");
                }

                

            }

            return health <= 0;
        }
    }

    float maxHealth;

    private Vector2 startPos;

    



    public override void Start () {



        
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Level1")
        {
            GameControl.control.trademark = 1;
        }

        if (scene.name == "Level2" || scene.name == "Level3" || scene.name == "Level4" || scene.name == "Level5" || scene.name == "Level6" || scene.name == "Level7" || scene.name == "Level8" || scene.name == "Level9")
        {
            if (GameControl.control.trademark == 1)
            {
                trademarkCanvas.gameObject.SetActive(true);
            }
        }



        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        
        MyRigidbody = GetComponent<Rigidbody2D>();

        

        score = 0;

        
        Debug.Log(scene.name);

        if (GameControl.control.trademark == 0)
        {
            maxHealth = 50;
            health = Convert.ToInt32(maxHealth);
        }

        if (GameControl.control.trademark == 1)
        {
            maxHealth = 100;
            health = Convert.ToInt32(maxHealth);
        }

    }

    private void Update()
    {

        Timer += Time.fixedDeltaTime;

        if (!TakingDamage && !IsDead)
        {
            //if(transform.position.y <= -14f)
            //{
            //    Death();
            //}

            HandleInput();

            
        }



        HealthBar.healthBar.fillAmount = health / maxHealth;

    }

 
    void FixedUpdate () {

        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            if (move)
            {
                this.btnHorizontal = Mathf.Lerp(btnHorizontal, direction, Time.deltaTime * accelerationSpeed);
                HandleMovement(btnHorizontal);
                Flip(direction);
            }
            else
            {
                Flip(horizontal);
                HandleMovement(horizontal);
            }


            HandleLayers();
        }

   
	}

    public void OnDead()
    {
        if(Dead != null)
        {
            Dead();
        }
    }
    
    private void HandleMovement(float horizontal)
    {
        if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if(!Attack && !Slide && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if(Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("slide");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("throw");
        }

    }

    private void Flip(float horizontal)
    {

        if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Slide"))
        {
            if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
            {
                ChangeDirection();
            }
        }

    }



    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y <= 0)
        {
            foreach  (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {

                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowKnife(value);   
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {

            health -= 10;


            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }

        }

    }

    //private IEnumerator DeathWait()
    //{
    //    while (IsDead)
    //    {

    //        yield return new WaitForSecondsRealtime(5);
            
    //    }
    //}

    public override void Death()
    {
        //StartCoroutine(DeathWait());

        //Debug.Log("CouroutineStarted");

        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = Convert.ToInt32(maxHealth);
        transform.position = startPos;

        GameControl.control.coins -= score;
        score = 0;
        SceneManager.LoadScene("GameOver");

        if (scene.name == "Level3" || scene.name == "Level4" || scene.name == "Level5" || scene.name == "Level6" || scene.name == "Level7" || scene.name == "Level8" || scene.name == "Level9")
        {
            MyRigidbody.velocity = Vector2.zero;
            MyAnimator.SetTrigger("idle");
            health = Convert.ToInt32(maxHealth);
            transform.position = startPos;

            GameControl.control.coins -= score/2;
            score = 0;
            //SceneManager.LoadScene("Menu");
            SceneManager.LoadScene("GameOver");
        }


    }

    
    public  void Reviwe()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = Convert.ToInt32(maxHealth);
        transform.position = startPos;
        Player.score = 0;
    }

    public void RestoreFull()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = Convert.ToInt32(maxHealth);
        transform.position = startPos;
        
    }

    public void RestoreHalf()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = Convert.ToInt32(maxHealth/2);
        transform.position = startPos;
        
    }

    public void BtnJump()
    {
        MyAnimator.SetTrigger("jump");
        Jump = true;
        Jump = false;
    }

    public void BtnAttack()
    {
        MyAnimator.SetTrigger("attack");
    }

    public void BtnSlide()
    {
        MyAnimator.SetTrigger("slide");
    }

    public void BtnThrow()
    {
        MyAnimator.SetTrigger("throw");
    }

    public void BtnMove(float direction)
    {
        this.direction = direction;
        this.move = true;
    }

    public void BtnStopMove()
    {
        this.direction = 0;
        this.btnHorizontal = 0;
        this.move = false;
    }

    public void OnFullHealth()
    {
        if (GameControl.control.coins >= 50)
        {
            GameControl.control.coins -= 50;
            RestoreFull();
            portionCanvas.gameObject.SetActive(false);
            Debug.Log("Full");
            Debug.Log(GameControl.control.coins);
        }

        else if (GameControl.control.coins < 50)
        {
            portionText.text = "Sorry, you don't have sufficient coins";
        }

    }

    public void OnHalfHealth()
    {
        if (GameControl.control.coins >= 30)
        {

            GameControl.control.coins -= 30;
            RestoreHalf();
            portionCanvas.gameObject.SetActive(false);
            Debug.Log("Half");
            Debug.Log(GameControl.control.coins);
        }
        
        else if(GameControl.control.coins < 30)
        {
            portionText.text = "Sorry, you don't have sufficient coins";
            Debug.Log(GameControl.control.coins);
        }
    }


    public void OnDontRevive()
    {
        OnDead();
        portionCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene("GameOver");
    }

    public void YesTrademark()
    {
        if(scene.name == "Level2")
        {
            GameControl.control.coins -= 30;
            trademarkCanvas.gameObject.SetActive(false);
        }
        else
        {
            GameControl.control.trademark = 1;
            trademarkCanvas.gameObject.SetActive(false);
            GameControl.control.coins -= 20;
        }

    }

    public void NoTrademark()
    {
        GameControl.control.trademark = 0;
        trademarkCanvas.gameObject.SetActive(false);
        Death();
        SceneManager.LoadScene("GameOver");
    }

}
