using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour
{
    public string username;
    public string roomname;

    public TMP_InputField ad;
    public TMP_InputField odaadi;
    public GameObject main, select;

    private void Start()
    {
        main.SetActive(true);

        select.SetActive(false);
    }
    private void Update()
    {
        username = ad.text;
        roomname = odaadi.text;
    }
    public void Enter()
    {
        PlayerPrefs.SetString("name", username);
        PlayerPrefs.SetString("room", roomname);
        main.SetActive(false);

        select.SetActive(true);


    }

    public void BlueTeam()
    {
        PlayerPrefs.SetString("player", "PlayerBlue");
        SceneManager.LoadScene(1);
    }
    public void RedTeam()
    {
        PlayerPrefs.SetString("player", "PlayerRed");
        SceneManager.LoadScene(1);
    }

}