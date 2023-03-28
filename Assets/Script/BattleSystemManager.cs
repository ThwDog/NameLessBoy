using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemManager : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST }

    private GameObject enemy;

    public Transform enemyBattlePosition;

    public StatusData playerStatus;
    public SceneLoad data;
    public StatusData[] dataId;

    public StatusHub playerStatusHUD;
    public StatusHub enemyStatusHUD;

    private BattleState battleState;

    [SerializeField] private bool hasClicked = true;

    [Header("Button")]
    public Button attck;
    public Button heal;
    public Button blocks;
    public Button hardAtk;

    [Header("hardAtkCount")]
    int sAtkCount = 0;
    [SerializeField] bool _canHAtk = true;

    public SpriteRenderer bg;
    public Sprite[] bgImg;

    private void Awake()
    {
        data = SceneLoad.instance;
        bg = GameObject.Find("BG").GetComponent<SpriteRenderer>();

        if (data.scene == 1 || data.scene == 2) bg.sprite = bgImg[0];
        else if (data.scene == 3) bg.sprite = bgImg[1];
    }

    void Start()
    {
        Debug.Log(dataId[data.id]);
        battleState = BattleState.START;
        StartCoroutine(BeginBattle());
    }

    private void Update()
    {
        if(sAtkCount >= 1) StartCoroutine(reHardAtk());

        if (hasClicked)
        {
            attck.interactable = false;
            heal.interactable = false;
            blocks.interactable = false;
            hardAtk.interactable = false;
        }
        else
        {
            attck.interactable = true;
            heal.interactable = true;
            blocks.interactable = true;

            if(_canHAtk) hardAtk.interactable = true;
            else hardAtk.interactable = false;
        }
    }



    IEnumerator reHardAtk()
    {
        _canHAtk = false;
        yield return new WaitForSeconds(10);
        sAtkCount = 0;
        _canHAtk = true;
    }

    IEnumerator BeginBattle()
    {
        // spawn characters on the platforms
        enemy = Instantiate(dataId[data.id].characterGameObject, enemyBattlePosition); enemy.SetActive(true);
        //player = Instantiate(playerStatus.characterGameObject.transform.GetChild(0).gameObject, playerBattlePosition); player.SetActive(true);

        // make the characters sprites invisible in the beginning

        // set the characters stats in HUD displays
        playerStatusHUD.SetStatusHUD(playerStatus);
        enemyStatusHUD.SetStatusHUD(dataId[data.id]);

        yield return new WaitForSeconds(1);


        yield return new WaitForSeconds(2);

        // player turn!
        battleState = BattleState.PLAYERTURN;

        // let player select his action now!    
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1);
 
        hasClicked = false;
    }
    public void attackClick()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
            StartCoroutine(PlayerAttack());

            hasClicked = true;
        }
    }

    public void healCilck()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
            StartCoroutine(playerHeal());

            hasClicked = true;
        }
    }

    public void blockClick()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
            StartCoroutine(block());

            hasClicked = true;
        }
    }
    IEnumerator block()
    {
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(PlayerTurn());
        yield return new WaitForSeconds(1);


    }

    public void specialAtk()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;
        if (!hasClicked)
        {
            StartCoroutine(sAtk());

            hasClicked = true;
        }
    }

    IEnumerator sAtk()
    {
        yield return new WaitForSeconds(2);

        float specialAtk = playerStatus.damage * 3;

        enemyStatusHUD.SetHP(dataId[data.id], specialAtk);

        sAtkCount++;

        if (dataId[data.id].health <= 0)
        {
            battleState = BattleState.WIN;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator playerHeal()
    {
        yield return new WaitForSeconds(2);

        playerStatusHUD.SetHPHeal(playerStatus, 20);

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(2);

        enemyStatusHUD.SetHP(dataId[data.id], playerStatus.damage);

        if (dataId[data.id].health <= 0)
        {
            battleState = BattleState.WIN;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        playerStatusHUD.SetHP(playerStatus, dataId[data.id].damage);

        yield return new WaitForSeconds(2);

        if (playerStatus.health <= 0)
        {
            battleState = BattleState.LOST;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            yield return StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator EndBattle()
    {
        if (battleState == BattleState.WIN)
        {
            // you may wish to display some kind
            // of message or play a victory fanfare
            // here
            yield return new WaitForSeconds(1);
            if(SceneLoad.instance.scene == 1)
            {
                SceneLoad.instance.levelLoad("World_2");
            }
            if (SceneLoad.instance.scene == 2)
            {
                SceneLoad.instance.levelLoad("World_Boss");
            }
            if (SceneLoad.instance.scene == 3)
            {
                Application.Quit();
            }
        }
        // otherwise check if we lost
        // You probably want to display some kind of
        // 'Game Over' screen to communicate to the 
        // player that the game is lost
        else if (battleState == BattleState.LOST)
        {
            // you may wish to display some kind
            // of message or play a sad tune here!
            yield return new WaitForSeconds(1);
            if(SceneLoad.instance.scene == 1)
            {
                SceneLoad.instance.levelLoad("World_1");
            }
            if (SceneLoad.instance.scene == 2)
            {
                SceneLoad.instance.levelLoad("World_2");
            }
            if (SceneLoad.instance.scene == 3)
            {
                SceneLoad.instance.levelLoad("World_Boss");
            }
        }
    }
}
