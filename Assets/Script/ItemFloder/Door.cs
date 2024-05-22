using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    ScenesMove scenes;

    bool isOpen;

    public bool IsOpen
    {
        get { return isOpen; }
        set 
        { 
            isOpen = value;
            animator.SetTrigger("Open");

        }
    }    

    private void Start()
    {
        animator = GetComponent<Animator>();
        scenes = GetComponent<ScenesMove>();
    }
    public void Active()
    {
        IsOpen = true;
        StartCoroutine(ScenesCo());
    }

    IEnumerator ScenesCo()
    {
        while (IsOpen) 
        { 
            yield return new WaitForSeconds(1.5f);
            scenes.GameScenes("END");
        
        }
    }

}
