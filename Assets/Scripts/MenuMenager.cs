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

    private void Update()
    {
        username = ad.text;
        roomname = odaadi.text;
    }
    public void Enter()
    {
        PlayerPrefs.SetString("name", username);
        PlayerPrefs.SetString("room", roomname);
        SceneManager.LoadScene(1);
    }


}