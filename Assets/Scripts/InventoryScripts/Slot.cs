using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;

    public Sprite icon;
    public bool empty; //variable para validacion

    public Transform SlotIconGameObject;

    void Start() 
    {
        SlotIconGameObject = transform.GetChild(0);
    }

    void Update() 
    {
        //Equipamos el item al jugador con la letra Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseItem();
        }
    }
    
    //actualizamos el sprite del slot
    public void UpdateSlot()
    {
        SlotIconGameObject.GetComponent<Image>().sprite = icon;
    }

    //Nos permite equipar el item que escojamos
    public void UseItem()
    {
        item.GetComponent<Items>().ItemUsage();
    }

    /*Al momento de dar click sobre el icono del objeto del inventario, llamamos al metodo  
    UseItem el cual nos permite usar el objeto sobre el juador*/
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }

}
