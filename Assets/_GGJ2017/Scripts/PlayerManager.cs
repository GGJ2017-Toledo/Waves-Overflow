using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerManager : MonoBehaviour
{
    public int acertos;
    public int erros;
    public int total;
    public GameObject sharkao;
    public GameObject waves;
    public GameObject pomba;

    public AudioSource audioSource;
    public readonly Color colFail = Color.red;
    public readonly Color colWin = Color.green;
    public readonly Color colNormal = Color.white;

    private float lerp =0f ;
    private float poswaveinicial;
    private float poswavefinal;

    private int teclaAtual;
    private bool acertou;
    private bool pressed;
    private bool jogando;

    public float bpm;
    private float time;

    public GameObject teclaAtualTexto;
    public GameObject lifeBar;
    public Material charGlow;

    public int maxHealth = 15;
    private int health;
    public int acertosTrigger = 3;
    public int acertosConsecutivos;

    void Awake()
    {   
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        jogando = true;
        poswaveinicial = waves.transform.position.x;        
        poswavefinal = poswaveinicial;
        acertos = 0;
        erros = 0;
        total = 0;    
        charGlow.EnableKeyword("_EMISSION");
        teclaAtual = getKey();
        health = maxHealth;
        acertou = false;
        pressed = false;
        time = 60 / bpm;
        InvokeRepeating("CheckBeat", 0, time);        
        Invoke("musicaFim", audioSource.clip.length);
    }

    public void musicaFim()
    {        
        // Application.Quit();

        Application.LoadLevel("End_Win");
    }

    public void perder()
    {
        jogando = false;
        poswavefinal = 4.15f;
        Invoke("pegueOPombo",1f);

        
    }

    public void pegueOPombo()
    {
        pomba.SetActive(false);
        Invoke("proximacena", 1f);
    }

    void proximacena()
    {
        Application.LoadLevel("End_Lose");
    }

    void getInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (teclaAtual == 0 && !pressed)
                acertou = true;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (teclaAtual == 1 && !pressed)
                acertou = true;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (teclaAtual == 2 && !pressed)
                acertou = true;
            pressed = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (teclaAtual == 3 && !pressed)
                acertou = true;
            pressed = true;
        }        
    }

    void ganhaVida()
    {
        health++;        
    }

    void perdeVida()
    {
        health--;
    }

    void Update()
    {

        if (jogando)
            poswavefinal = -4.15f - 0.31f * (health - maxHealth);

        getInput();        
        lerp = Time.deltaTime / (time);
        poswaveinicial = Mathf.Lerp(poswaveinicial, poswavefinal, lerp);
        waves.transform.position = new Vector2(poswaveinicial, 0);

        if (pressed)
            if (acertou)
            {
                teclaAtualTexto.GetComponent<Image>().color = colWin;
                charGlow.SetColor("_EmissionColor", new Color(0.17f, 3, 0));                            
            }
            else
            {                                
                teclaAtualTexto.GetComponent<Image>().color = colFail;
                charGlow.SetColor("_EmissionColor", new Color(3, 0, 0));

            }        
        else
        {
            teclaAtualTexto.GetComponent<Image>().color = colNormal;
            charGlow.SetColor("_EmissionColor", new Color(3, 3, 3));

        }                    
    }

    public void CheckBeat()
    {    

        total++;

        if (acertou)
        {
            acertosConsecutivos++;
            acertos++;
        }
        else
        {
            acertosConsecutivos = 0;            
            if (health > 0)
                perdeVida();
            else
            {
                sharkao.SetActive(true);
                perder();
            }
            erros++;
        }

        if(acertosConsecutivos == 3)
        {
            acertosConsecutivos = 0;
            if (health < maxHealth)
                ganhaVida();
        }

        acertou = false;
        pressed = false;
        teclaAtual = getKey();
        teclaAtualTexto.GetComponentInChildren<Text>().text = valTecla().ToString();
        Invoke("turnOff", 0.1f);
    }

    public void onOnbeatDetected()
    {
        CheckBeat();
    }
   
    public int getKey()
    {
        return UnityEngine.Random.Range(0, 4);
    }

    public char valTecla()
    {
        if (teclaAtual == 0)
            return 'D';
        if (teclaAtual == 1)
            return 'F';
        if (teclaAtual == 2)
            return 'J';
        if (teclaAtual == 3)
            return 'K';
        return ' ';
    }
}
