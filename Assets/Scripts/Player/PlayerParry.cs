using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerParry : SaiMonoBehavior
{
    [Header("Parry Mechanic")]
    [SerializeField] Collider2D parryCollider;
    [SerializeField] float parryForce;
    [SerializeField] float parryDuration;
    private Coroutine currentParryState = null;
    internal bool isParry;
    internal int parryCounter = 0;
    internal bool isCounter;
    private int enemyLayer = 7;
    private bool consecutiveParry;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadParryComponents();
    }
    private void LoadParryComponents()
    {
        parryForce = 50;
        isParry = false;
        isCounter = false;
    }
    internal void CheckForParry()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isParry)
        {
            currentParryState = StartCoroutine(ParryState());
        }
    }
    protected IEnumerator ParryState()
    {
        isParry = true; //cue for parry animation

        yield return new WaitForSeconds(0.1f); //time for parry animation to finish

        parryCollider.enabled = true;

        yield return new WaitForSeconds(parryDuration);

        parryCollider.enabled = false;
        if (!isCounter) //if isn't counter anything, then turn off consecutive parry flow
        {
            consecutiveParry = false;
        }

        yield return new WaitForSeconds(0.65f);
        isParry = false;
    }
    internal IEnumerator Parry(Collider2D collision)
    {
        if (collision.gameObject.layer != enemyLayer || PlayerController.instance.playerDeath.isDead) yield break;

        //Phase 1: setting up player conditions for parry
        PlayerController.instance.playerCollision.allowCollision = true; //player become immune to enemy
        PlayerController.instance.playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        //Phase 2: initiating the attack animation and stopping the game for a moment to emphasize the effect
        isCounter = true; //cue for counter attack animation
        if (!consecutiveParry)
        {
            HitStop.instance.Stop(0.125f);
        }

        yield return new WaitForSeconds(0.09f);

        //Phase 3: obliterating the enemy
        Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
        Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;
        enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
        enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
        //collision.gameObject.layer = 6;
        isParry = false;
        StopCoroutine(currentParryState);
        isCounter = false;
        parryCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        if (isParry)
        {
            print("Consecutive Parry");
            consecutiveParry = true;
            PlayerController.instance.playerCollision.allowCollision = false;
            parryCounter++;
            yield break;
        }

        //Phase 4: setting every conditions back to normal
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false;
        parryCounter++;
    }
}
