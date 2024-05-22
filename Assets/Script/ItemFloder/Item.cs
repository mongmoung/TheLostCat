using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public abstract class Item : MonoBehaviour
{
    
    public Sprite sprite;
    public string itemName;

    public abstract void Use(Player target);//슬롯 스크립트에서 use를 호출



}
