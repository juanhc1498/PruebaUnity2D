using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Variables de velocidad de movimiento
    public float Speed = 0.0f, SpeedRotation = 250.0f;

    //Variables para animaciones y direccion
    Animator Anim;
    public float x, y;

    //Variables que queremos guardar
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //variables de direcciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        //movimiento y rotacion
        transform.Rotate(0, x * Time.deltaTime * SpeedRotation, 0);
        transform.Translate(0, 0, y * Time.deltaTime * Speed);

        //Animaciones
        Anim.SetFloat("SpeedX", x);
        Anim.SetFloat("SpeedY", y);

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
    }

    //Metodo que vamos a usar para guardar
    void Save()
    {   
        //Creamos el objeto Json y le asignamos una clave
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Name", playerName.text);
        
        /*Guardamos la posicion con un Vector3, debido a que el desarrollo de este proyecto
        es 3D*/
        JSONArray playerPosition = new JSONArray();
        playerPosition.Add(transform.position.x);
        playerPosition.Add(transform.position.y);
        playerPosition.Add(transform.position.z);

        //Lo agregamos a playerJson
        playerJson.Add("Position", playerPosition);

        //Guardamos el Json en el pc con un archivo de texto
        //creamos la ruta donde lo vamos a almacenar
        string path = Application.dataPath +  "/PlayerSave.json";

        //creamos el archivo donde se almacenara
        File.WriteAllText(path, playerJson.ToString());
    }

    //Metodo que vamos a usar para guardar
    void Load()
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
            playerJson["Position"].AsArray[1],
            playerJson["Position"].AsArray[2]
        );
    }
}
