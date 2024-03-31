using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerParry : SaiMonoBehavior
{
    [Header("Parry Mechanic")]
    [SerializeField] internal Collider2D parryCollider;
    [SerializeField] float parryForce;
    private float parryDuration;
    private Coroutine currentParryState = null;
    internal bool isParry;
    internal bool isCounter;
    internal bool isSpecialParry;

    [Header("Parry Related Conditions Variables")]
    internal int parryCounter = 0;
    private const int enemyLayer = 7;
    private const int playerLayer = 6;
    internal bool consecutiveParry;
    internal bool gotSpecialParried; //check if the enemy got special parried or not

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadParryComponents();
    }
    private void LoadParryComponents()
    {
        parryForce = 60;
        parryDuration = 0.3f;
        isParry = false;
        isCounter = false;
    }
    internal void CheckForParry()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isParry)
        {
            currentParryState = StartCoroutine(EnterParryState());
        }
    }
    protected IEnumerator EnterParryState()
    {
        isParry = true; //cue for parry animation
        
        yield return new WaitForSecondsRealtime(0.06f); //parry prepare time
        parryCollider.enabled = true;

        yield return new WaitForSeconds(parryDuration);

        parryCollider.enabled = false;
        if (!isCounter) consecutiveParry = false; //if isn't countering anything, then turn off consecutive parry flow

        yield return new WaitForSecondsRealtime(0.64f);

        isParry = false;
    }
    internal IEnumerator Parry(GameObject enemyObject)
    {
        if (enemyObject.layer != enemyLayer || PlayerController.instance.playerDeath.isDead) yield break;

        //Phase 1: setting up player conditions for parry
        PlayerController.instance.playerCollision.allowCollision = true; //player become immune to enemy contact
        PlayerController.instance.playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        //Phase 2: initiating the attack animation and stopping the game for a moment to emphasize the effect
        isCounter = true; //cue for counter attack animation
        if (!consecutiveParry && !PlayerController.instance.playerZone.inZone) HitStop.instance.Stop(0.125f);
        //hit stop shouldn't occur when player is in zone 

        yield return new WaitForSeconds(0.09f);

        //Phase 3: obliterating the enemy
        ParryKnockBack(enemyObject);
        //print("Is parry");

        //Phase 4: setting up conditions upon exiting parry 
        TurnOffParryConditions();

        yield return new WaitForSecondsRealtime(0.1f);

        enemyObject.layer = playerLayer; //turn enemy into player's projectile after deflect them
        CheckIfConsecutiveParry();
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false; //turn on player vulnerability again
        parryCounter++;
    }
    internal IEnumerator SpecialParry(GameObject enemyObject) //only activate during or after zone 
    {
        if (enemyObject.layer != enemyLayer || PlayerController.instance.playerDeath.isDead) yield break;

        PlayerController.instance.playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        PlayerController.instance.playerCollision.allowCollision = true;

        isSpecialParry = true;

        yield return new WaitForSeconds(0.09f);
        
        ParryKnockBack(enemyObject);
        enemyObject.layer = playerLayer; //turn on layer immediately to ensure the explosion collision

        yield return new WaitForSeconds(0.1f);
        isSpecialParry = false;
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false; //turn on player vulnerability again
    }

    private void ParryKnockBack(GameObject enemyObject)
    {
        if(isParry && isCounter)
        {
            Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
            enemyRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //prevent object from phasing
            Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;
            enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
            enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
        }
        if (isSpecialParry)
        {
            Collider2D groundCollider = GameObject.Find("Ground").GetComponent<Collider2D>();
            Collider2D enemyCollider = enemyObject.GetComponent<Collider2D>();
            var targetEnemy = FindClosestTarget.Instance.FindClosestEnemy(enemyObject.transform);
            //print("Targeted Enemy Position: " + targetEnemy.transform.position);

            Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
            Vector2 enemyDir = targetEnemy.transform.position - transform.parent.position;
            Physics2D.IgnoreCollision(enemyCollider, groundCollider);
            enemyRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //prevent object from phasing

            enemyRb.velocity = Vector2.zero; //reset velocity to avoid unwanted deflect direction
            enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
            enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
            gotSpecialParried = true;
        }
    }

    private void TurnOffParryConditions()
    {
        isParry = false;
        isCounter = false;
        StopCoroutine(currentParryState); //stop the ongoing function of parry state, it will mess with the flow of the player functionality if left untouch 
        parryCollider.enabled = false;
    }

    private void CheckIfConsecutiveParry()
    {
        if (!isParry) return;
        consecutiveParry = true;
    }
}
