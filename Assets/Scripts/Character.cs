using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Character : MonoBehaviour {

    public Image winImage;

    //-----------------------------

    public Canvas question;
    public Text questionText;

    public Button option1;
    public Button option2;


    //-----------------------------

    [SerializeField]
    protected Transform knifePos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    protected GameObject knifePrefab;

    [SerializeField]
    protected int health;

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }


    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimator { get; private set; }

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }

    }

    //------------------------------------
    public GameObject littleGirl;
    public GameObject littleGirlDialogueBox;

    public Canvas catCanvas;
    
    //------------------------------------


    // Use this for initialization
    public virtual void Start () {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();

    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract IEnumerator TakeDamage();

    public abstract void Death(); 

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, +90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
    }

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }

        if (other.gameObject.CompareTag("littleGirl"))
        {
            littleGirlDialogueBox.SetActive(true);
            littleGirl.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

        if (other.gameObject.CompareTag("cat"))
        {
            catCanvas.gameObject.SetActive(true);
            catDialogue.hasCollided = 1;
        }

        if (other.gameObject.CompareTag("Finish") )
        {
            winImage.gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("DeathBorder"))
        {
            Time.timeScale = 0;
            question.gameObject.SetActive(true);
            //Death();
        }

    }



    public void OnOption1Click()
    {
        Player.Instance.Reviwe();


        question.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnOption2Click()
    {
        //string sceneNo = SceneManager.GetActiveScene().ToString();
        //SceneManager.LoadScene(sceneNo);
        Death();
   

        question.gameObject.SetActive(false);
        Time.timeScale = 1;
    }



    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("littleGirl"))
        {
            littleGirlDialogueBox.SetActive(false);
        }

    }

}
