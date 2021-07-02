using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //variables informacion del objeto
    public int ID;
    public string type;
    public string description;
    public Sprite icon;

    [HideInInspector]
    public bool IOnPickedup;

    [HideInInspector]
    public bool Iequipped;

    [HideInInspector]
    public GameObject objectManager;

    [HideInInspector]
    public GameObject objectM;    

    public bool IPlayerObjects;


    private void Start() 
    {
        /*Buscamos el objeto que nos administra los objetos recogidos en la escena, el cual es hijo 
        del player por medio del tag*/
        objectManager = GameObject.FindWithTag("ObjectsManager");


        if (!IPlayerObjects)
        {

            //Referenciamos los objetos que tenemos dentro del objectManager
           int allObjects = objectManager.transform.childCount;

            /*nos aseguramos que los elementos tengan el mismo ID, el que cogimos junto con el que vamos
             a usar*/
            for (int i = 0; i < allObjects; i++)
            {
                //para saber que objeto tenemos que activar en la mano
                if(objectManager.transform.GetChild(i).gameObject.GetComponent<Items>().ID == ID)
                {
                    objectM = objectManager.transform.GetChild(i).gameObject;
                }
            }

        }

    }

    
    private void Update() 
    {
        //Desequipamos los objetos que tenemos presionando la tecla E
        if (Iequipped)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Iequipped = false;
            }

            if (Iequipped == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    //Usamos los objetos del inventario
    public void ItemUsage()
    {
        //activar y equipar
        if (type == "Element")
        {
            objectM.SetActive(true);
            objectM.GetComponent<Items>().Iequipped = true;
        }

    }
}
