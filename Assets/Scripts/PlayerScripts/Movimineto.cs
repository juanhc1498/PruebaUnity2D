using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using SimpleJSON;
using System.IO;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
public class Movimineto : MonoBehaviour
{

    //variables
    Rigidbody2D RB;
    Animator Anim;
    public float Velocidad;
    bool voltear = true;

    //Variables que queremos guardar
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mover = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2(mover * Velocidad,RB.velocity.y);
        Anim.SetFloat("VelMax", Mathf.Abs(mover));

        //girar a la derecha
        if (mover > 0 && !voltear) 
        {
            voltear = !voltear;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        } 
        
        //girar a la izquierda
        else if (mover < 0 && voltear) 
         {
            voltear = !voltear;
            Vector3 escala = transform.localScale;
            escala.x *= -1;//para que gire
            transform.localScale = escala;           
        }

         /*Condicionales que me permiten guardar pulsando la tecla K y cargar pulsando la tecla L
        haciendo un llamado a cada uno de los metodos*/
        if(Input.GetKeyDown(KeyCode.K))
        {
            Save();
            Debug.Log("Save");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
            Debug.Log("Load");
        }

        //ANIMACIONES 
        //animacion recoger objetos
        if (Input.GetKey(KeyCode.R))
        {
            Anim.SetBool("Recoger", false);
        }
        else
        {
            Anim.SetBool("Recoger", true);
        }

    }

    //Metodo que vamos a usar para guardar
    public void Save()
    {   
        //Creamos el objeto Json y le asignamos una clave
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Name", playerName.text);
        
        /*Guardamos la posicion con un Vector3*/
        JSONArray playerPosition = new JSONArray();
        playerPosition.Add(transform.position.x);
        //playerPosition.Add(transform.position.y);

        //Lo agregamos a playerJson
        playerJson.Add("Position", playerPosition);

        //Guardamos el Json en el pc con un archivo de texto
        //creamos la ruta donde lo vamos a almacenar
        string path = Application.dataPath +  "/PlayerSave.json";

        //creamos el archivo donde se almacenara
        File.WriteAllText(path, playerJson.ToString());
    }

    //Metodo que vamos a usar para guardar
    public void Load()
    {
        //Cargamos los datos guardados accediendo de nuevo a la ruta que habiamos creado
        string path = Application.dataPath +  "/PlayerSave.json";

        //Volvemos a llamar al contenido del archivo Json creado, es decir al texto de este
        string jsonString = File.ReadAllText(path);

        //Convertimos nuestro archivo Json a cadena de texto para que pueda ser leido
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

        /*Finalmente, asignaremos un valor a cada clave que fue creada anteriormente en el metodo Save()
        en este caso, solo usamos el nombre, es decir, en Save() estaba "playerJson.Add("Name", playerName.text)";
        ahora sera al contrario para poder ser leido Name = playerJson["Name"]*/
        playerName.text = playerJson["Name"];

        /*Para cargar la posicion, es algo diferente, debido a que es un array, por ende al final lo pasamos
        como un array, donde cada numero (0, 1, 2) representan los ejes (x, y, z)*/
        transform.position = new Vector3(
            playerJson["Position"].AsArray[0],
            playerJson["Position"].AsArray[0]
        );
    }

}
