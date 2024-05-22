using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Progress;

public enum ITEM_TYPE
{
    ESCAPE,
    MESSAGE,
    STAMINABUFF,
    CONFUSION

}

public  class UseItemStrategy
{
    public UseItem useItem;
    public UseItemStrategy(UseItem useItem)
    {
        this.useItem = useItem;       
    }

    public virtual void Active(Player player) { }
}

public class ConfusionItem : UseItemStrategy
{

    public ConfusionItem(UseItem useItem) : base(useItem) 
    {
        useItem.gameObject.layer = 0;
    }

    public override void Active(Player player)
    {
        useItem.gameObject.SetActive(true);
        useItem.gameObject.layer = 9 ;
        useItem.rb.bodyType = RigidbodyType2D.Dynamic;
        useItem.rb.AddRelativeForce(useItem.transform.up * 5, ForceMode2D.Impulse);
        useItem.rb.AddRelativeForce(player.transform.right * 5, ForceMode2D.Impulse);
        useItem.gameObject.transform.SetParent(null);
        useItem.StartCoroutine(ConfusionCo());
    }
    public IEnumerator ConfusionCo()
    {
        while (useItem.gameObject.activeSelf == true)
        {
            yield return new WaitForSeconds(5);
            GameObject.Destroy(useItem.gameObject);

        }
    }
}

public class EscapeItem : UseItemStrategy
{

    public EscapeItem(UseItem useItem) : base(useItem) { }
    public override void Active(Player player)
    {
    
        useItem.gameObject.SetActive(true);
        Vector2 upKey = new Vector2 (useItem.transform.position.x, player.transform.position.y + 0.5f);
        useItem.gameObject.transform.position = upKey;

        useItem.StartCoroutine(WaitForTime());

    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1.5f);
        int randomDoor = Random.Range(1, 5);

        switch (randomDoor)
        {

            case 1:
                Debug.Log(useItem.name);
                SoundManager.instance.SFXPlay("Door", useItem.clip);    
                GameObject.Destroy(useItem.gameObject);
                useItem.door.Active();

                break;
            case 2:
                Debug.Log("문열려는 사운드");//문 달칵달칵 하는 사운드
                break;
            case 3:
                Debug.Log("문열려는 사운드");//문 달칵달칵 하는 사운드
                break;
            case 4:
                Debug.Log("문열려는 사운드");//문 달칵달칵 하는 사운드
                break;


        }
    }

   
}

public class MessageItem : UseItemStrategy
{
    MessageTXT messageTXT;


    public MessageItem(UseItem useItem) : base(useItem)
    {
        messageTXT = useItem.GetComponentInChildren<MessageTXT>(true);
    }
    public override void Active(Player player)
    {
        player.playerController.IsMove = false;
        useItem.GetComponent<Collider2D>().enabled = false;
        useItem.GetComponent<Renderer>().enabled = false;
        useItem.gameObject.SetActive(true);
        messageTXT.messageText.text = messageTXT.GetMessage(useItem.id);
        useItem.StartCoroutine(CancellMassegeCo(player));
        


    }

    IEnumerator CancellMassegeCo(Player player)//야매코딩
    {
        while (useItem.gameObject.activeSelf ==true)
        {
            yield return new WaitForSeconds(0.01f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                useItem.gameObject.SetActive(false);
                player.playerController.IsMove = true;
            }
        }
        yield return null;
    }
}

public class StaminaBuff : UseItemStrategy
{
    public StaminaBuff(UseItem useItem) : base(useItem) { }
    public override void Active(Player player)
    {
        player.Stamina += 20f;
    }

}


public class UseItem : Item
{
    public Door door = null;
    public int id;
    public AudioClip clip;

    public ITEM_TYPE Item_Type;
    public CircleCollider2D circle;
    public Rigidbody2D rb;
    public UseItemStrategy UseItemStrategy;


    private void Start()
    {

        UseItemStrategy = new UseItemStrategy(this);
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();

        switch (Item_Type) 
        {
            case ITEM_TYPE.STAMINABUFF:
                UseItemStrategy = new StaminaBuff(this);
                break;

            case ITEM_TYPE.MESSAGE:
                UseItemStrategy = new MessageItem(this);
                break;

            case ITEM_TYPE.ESCAPE:
                UseItemStrategy = new EscapeItem(this);
                break;

            case ITEM_TYPE.CONFUSION:
                UseItemStrategy = new ConfusionItem(this);
                break;

        }
    }

    public override void Use(Player target)
    {
        UseItemStrategy.Active(target);    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Door>() != null)
        {
            door = collision.GetComponent<Door>();
        }
    }
}



