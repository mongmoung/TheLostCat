using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public abstract class Item : MonoBehaviour
{
    
    public Sprite sprite;
    public string itemName;

    public abstract void Use(Player target);//���� ��ũ��Ʈ���� use�� ȣ��



}
