using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum TRAP_TYPE
{
    DAMAGE,
    SURPRISED
}

public class TrapStrategy
{
    public Trap trap;

    public TrapStrategy(Trap trap)
    {
        this.trap = trap;
    }

    public virtual void Active(Player player) { }
}


public class DamageTrapStrategy : TrapStrategy
{
    public DamageTrapStrategy(Trap trap) : base(trap) { }


    public override void Active(Player player) 
    {
        SoundManager.instance.SFXPlay("Hit", trap.trapClip);
        player.Stamina -= trap.damage;
    }

}

public class SurprisedTrapStrategy : TrapStrategy
{
    Light2D light2D;
    public SurprisedTrapStrategy(Trap trap) : base(trap) 
    {
        light2D = trap.gameObject.GetComponentInChildren<Light2D>();
        light2D.intensity = 0;
    }

    public override void Active(Player player)
    {
        SoundManager.instance.SFXPlay("Surprise", trap.trapClip);
        light2D.intensity = 10;
        GameObject.Destroy(trap.gameObject.transform.GetChild(0).gameObject);
        trap.StartCoroutine(DestroyCo(player));
    }

    IEnumerator DestroyCo(Player player)
    {
        while(player != null)
        {
            yield return new WaitForSeconds(3);
            GameObject.Destroy(trap.gameObject);
        }
            
    }
}





public class Trap : MonoBehaviour
{

    public TrapStrategy trapStrategy;
    public TRAP_TYPE Trap_Type;
    public AudioClip trapClip;

    public int damage;

    private void Start()
    {
        trapStrategy = new TrapStrategy(this);
        switch (Trap_Type)
        {
            case TRAP_TYPE.DAMAGE:
                trapStrategy = new DamageTrapStrategy(this);
                break;

            case TRAP_TYPE.SURPRISED:
                trapStrategy = new SurprisedTrapStrategy(this);
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            trapStrategy.Active(player);
        }

    }
}
