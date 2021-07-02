using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptions : MonoBehaviour
{
    //Variables
    bool Iactive;
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Activar y desactivar el canvas de opciones
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Iactive = !Iactive;
            canvas.enabled = Iactive;
        }
    }
}
