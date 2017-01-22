using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScorePrint : MonoBehaviour 
{
    public Text _score;
    public int _finalScore;
    private GameObject _playerManager;

    void Start()
    {
        Invoke("VoltarProMenu", 5f);
        _playerManager = GameObject.Find("PlayerManager");
        _finalScore = _playerManager.GetComponent<PlayerManager>().acertos*5;
        _score.text = _finalScore.ToString();

        
    }

    void VoltarProMenu()
    {
        Destroy(_playerManager.gameObject);
        Application.LoadLevel("Start_Menu");
    }
}
