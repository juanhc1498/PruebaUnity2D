using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    //Variables
    //InputField display;
    public Text playername;
    public static string keepplayername;

    void Start() 
    {
        playername.text = PlayerPrefs.GetString("UserName");
    }

    //Obtenemos el nombre ingresado por el usuario, lo guardamos y lo colocamos en la siguiente escena
    public void CreateData()
    {
        //playername.text = display.text;
        PlayerPrefs.SetString("UserName", playername.text);
        PlayerPrefs.Save();
    }
}
