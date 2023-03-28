using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad instance;

    public Animator transition;
    [SerializeField]
    float tranistionTime;
    public int id;
    public PmovementCon data;
    public int scene;

    public UIControlll ui;
    void Awake()
    {   

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != this) Destroy(gameObject);

        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIControlll>();
    }

    public void levelLoad(string levelName)
    {
        StartCoroutine(loadlvl(levelName));
    }


    private void Start()
    {
            //data = GameObject.FindGameObjectWithTag("Player").GetComponent<PmovementCon>();
    }

    private void Update()
    {
        collectscene();
        id = data.idEnemy;

    }

    IEnumerator loadlvl(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(tranistionTime);
        SceneManager.LoadScene(levelName);
        transition.SetTrigger("End");
    }

    void collectscene()
    {
        Scene s = SceneManager.GetActiveScene();
        if(s.name == "World_1")
        {
            data = GameObject.FindGameObjectWithTag("Player").GetComponent<PmovementCon>();
            scene = 1;
            //Debug.Log("scene 1");
        }
        else if (s.name == "World_2")
        {
            data = GameObject.FindGameObjectWithTag("Player").GetComponent<PmovementCon>();
            scene = 2;
            //Debug.Log("scene 2");
        }
        else if (s.name == "World_Boss")
        {
            data = GameObject.FindGameObjectWithTag("Player").GetComponent<PmovementCon>();
            scene = 3;
            //Debug.Log("scene boss");
        }
    }
}
