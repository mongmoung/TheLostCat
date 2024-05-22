using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floor : MonoBehaviour
{

/*    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IWalkable>() != null)
            collision.gameObject.GetComponent<IWalkable>().Walk();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UseItem useItem = collision.gameObject.GetComponent<UseItem>();
        if (useItem != null && useItem.Item_Type == ITEM_TYPE.CONFUSION)
        {
            useItem.circle.isTrigger = false;
            StartCoroutine(RadiusUp(useItem));
        }
    }
   
    IEnumerator RadiusUp(UseItem useitem)
    {
        yield return new WaitForSeconds(1);
        Destroy(useitem.GetComponent<Rigidbody2D>());
        useitem.circle.radius = 10f;
    }

}    
