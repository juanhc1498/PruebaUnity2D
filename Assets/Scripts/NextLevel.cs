using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //public InputField display;

    //nombre de la siguiente escena
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);

        /*Con esto evitamos que el objeto que contiene el nombre del jugador, sea destruido al momento de 
        cambiar la escena*/
        //DontDestroy.keepplayername = display.text;
    }

    //cerrar app
    public void CloseApp()
    {
        Application.Quit();
    }
}
