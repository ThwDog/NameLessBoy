using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControlll : MonoBehaviour
{
    [SerializeField] Image settingImg;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject infoCanva;
    bool thisgamePause = false;

    [SerializeField] GameObject[] language;
    bool thai = true;
    [SerializeField]TextMeshProUGUI lan;

    public void Awake()
    {
        settingImg = GameObject.Find("Setting").GetComponent<Image>();      
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (thisgamePause)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        thisgamePause = true;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        infoCanva.SetActive(false);
        Time.timeScale = 1f;
        thisgamePause = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void info()
    {
        pauseMenu.SetActive(false);
        infoCanva.SetActive(true);
    }

    public void changeLaguage()
    {
        if(thai)
        {
            language[0].SetActive(false);
            language[1].SetActive(true);
            thai = false;
            lan.text = "Thai";
        }
        else if (!thai)
        {
            language[0].SetActive(true);
            language[1].SetActive(false);
            thai = true;
            lan.text = "Eng";

        }
    }


}
