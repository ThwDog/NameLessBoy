using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmovementCon : MonoBehaviour
{
    EnemyWeakPoint ewp;
    public StatusData player;
    
    Rigidbody2D rb;
    float horizontal;

    [Header("controll")]
    public float speed;
    public float sRun;

    public bool walk = false;

    [Header("Cam System")]
    [SerializeField] Transform mainCam;
    public float camFollSpeed;

    [Header("Score")]
    public int score;


    [Header("IDSet")]
    public int idEnemy;
    public StatusData[] dataId;

    GameObject ui;
    Animator anim;

    SpriteRenderer playerSprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ui = GameObject.Find("UI");
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        player.health = player.maxHealth;
    }

    
    void Update()
    {
        walking();
        camFoll();
    }

    void walking()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);


        if(horizontal > 0 || horizontal < 0)
        {
            walk = true;
            if(horizontal < 0) playerSprite.flipX = true;
            else playerSprite.flipX = false;
        }
        else
        {
            walk = false;
        }

        if(walk) anim.SetBool("Walk",true);
        else anim.SetBool("Walk", false);

        run();
    }

    void run()
    {
        float runSpeed;
        if(walk = true && Input.GetKey(KeyCode.LeftShift))
        {
            runSpeed = speed * sRun;
            rb.velocity = new Vector2(horizontal * runSpeed, rb.velocity.y);
        }
    }

    void pickUp()
    {
        if (Input.GetKey(KeyCode.F))
        {

        }
    }

    void camFoll()
    {
        Vector3 followPosition = new Vector3(transform.position.x, 0.5f, -10f);
        mainCam.position = Vector3.Lerp(mainCam.position, followPosition, Time.deltaTime * camFollSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {       
            if (other.tag == "Enemy")
            {
                
                player.position[0] = this.transform.position.x;
                player.position[1] = this.transform.position.y;

            //  setBattleData(other);
                //ui.SetActive(false);
                idEnemy = other.GetComponent<CharecterStatus>().type.id;
                Destroy(other.gameObject);

                Debug.Log(dataId[idEnemy]);     
                Debug.Log(dataId[idEnemy].damage);
                SceneLoad.instance.levelLoad("BattleScene");

        }
    }
  
    /*void setBattleData(Collider2D other)
    {       
        // Enemy Data
        StatusData status = other.GetComponent<CharecterStatus>().type;
        enemy.charname = status.charname;
        //enemy.characterGameObject = status.characterGameObject.transform.GetChild(0).gameObject;
    }*/

   
}
    
    


