using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    //variables
    bool InventoryEnabled;
    public GameObject inventory;
    int AllSlots;
    int EnabledSlots;
    GameObject[] slot;
    public GameObject SlotHolder;

    // Start is called before the first frame update
    void Start()
    {
        //numero de slots que tenemos
        AllSlots = SlotHolder.transform.childCount;
        slot = new GameObject[AllSlots];

        //metemos los slots al array
        for (int i = 0; i < AllSlots; i++)
        {
            slot[i] = SlotHolder.transform.GetChild(i).gameObject;

            //Comprobamos si el slot este vacio o no
            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //activamos y desactivamos el inventario
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            InventoryEnabled = !InventoryEnabled;
        }

        if (InventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        
        else
        {
            inventory.SetActive(false);
        }
    }

    //Colision para recoger los objetos
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPick = other.gameObject;
            Items items = itemPick.GetComponent<Items>();
            AddItems(itemPick, items.ID, items.type, items.description, items.icon);
        }
    }

    //metodo para añadir items a los slots, con su informacion
    public void AddItems(GameObject itemObject, int itemID, string itemtype, string itemdescription, Sprite itemicon)
    {

        //Añadimos los items, preguntando slot por slot si esta disponible
        for (int i = 0; i < AllSlots; i++)
        {
            //validamos si el slot esta vacio
            if (slot[i].GetComponent<Slot>().empty )
            {
                itemObject.GetComponent<Items>().IOnPickedup = true;

                //pasamos los datos de los items
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().type = itemtype;
                slot[i].GetComponent<Slot>().description = itemdescription;
                slot[i].GetComponent<Slot>().icon = itemicon;

                //colocamos el item dentro del slot
                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();

                //slot ocupado
                slot[i].GetComponent<Slot>().empty = false;

                return;
            }
        }
    }
}
