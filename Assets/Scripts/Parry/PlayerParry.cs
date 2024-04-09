using FirstGearGames.SmoothCameraShaker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerParry : BaseParry
{
    [SerializeField] private List<ParryShake> enemiesToShake = new List<ParryShake>();
    //a list of enemy to shake if the player roll through more than 1 enemy at a time

    private static bool isRunning = false; //check if there's any parry currently running
    private void Update()
    {
        print("Is Consecutive Parry: " + consecutiveParry);
    }
    protected override IEnumerator EnterParryState()
    {
        enemiesToShake.Clear();
        isParry = true; //cue for parry animation

        yield return new WaitForSecondsRealtime(0.06f); //parry prepare time

        parryCollider.enabled = true;

        yield return new WaitForSeconds(parryDuration);

        parryCollider.enabled = false;
        isRunning = false;
        if (!isCounter) consecutiveParry = false; //if isn't countering anything, then turn off consecutive parry flow

        yield return new WaitForSecondsRealtime(parryDuration);

        isParry = false;
    }
    internal override IEnumerator Parry(GameObject enemyObject)
    {
        if (enemyObject.layer != enemyLayer || PlayerController.instance.playerDeath.isDead) yield break;

        //Phase 1: setting up conditions for parry
        PlayerController.instance.playerCollision.allowCollision = true; //player become immune to enemy contact
        PlayerController.instance.playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        EnemyDeath enemyDeath = enemyObject.GetComponent<EnemyDeath>(); 
        

        //Phase 2: initiating the attack animation and stopping the game for a moment to emphasize the effect
        isCounter = true; //cue for counter attack animation

        yield return StartCoroutine(HitStopController(enemyObject)); 
        //wait until this coroutine is finished to continue with the function

        //Phase 3: obliterating the enemy
        ParryKnockBack(enemyObject);
        enemyDeath.isDead = true;
        enemyObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        //Phase 4: setting up conditions upon exiting parry 
        StartCoroutine(TurnOffParryConditions(enemyObject));
        GameManager.instance.ScoreUp(50, enemyObject.transform);
        parryCounter++;
    }
    protected override IEnumerator TurnOffParryConditions(GameObject enemyObject)
    {
        isParry = false;
        isCounter = false;
        StopCoroutine(currentParryState); //stop the ongoing function of parry state, it will mess with the flow of the player functionality if left untouch 
        parryCollider.enabled = false;

        yield return new WaitForSeconds(0.1f);

        enemyObject.layer = playerLayer; //turn enemy into player's projectile after deflect them
        CheckIfConsecutiveParry();
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false; //turn on player vulnerability again

        if (PlayerController.instance.playerZone.inZone) yield break;
        DifficultyScaling.Instance.DifficultyIncrease(); //increase difficulty after a successful parry
    }
    protected override void ParryKnockBack(GameObject enemyObject)
    {
        //Phase 1: Setting Up Variables
        Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
        Vector2 enemyDir = enemyRb.transform.position - transform.parent.position;

        //Phase 2: Adjusting Collision
        enemyRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //prevent object from phasing

        //Phase 3: Adding Knockback Forces
        enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
        enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
    }
    protected virtual void CheckIfConsecutiveParry()
    {
        if (!isParry || PlayerController.instance.playerRoll.isRolling) return;
        consecutiveParry = true;
    }
    protected virtual void EnemyReposition(GameObject enemyObject)
    {
        if (enemyObject.transform.position.x > transform.parent.position.x && PlayerController.instance.playerRoll.isRolling) return;

        //Reposition the enemy if they phase through the player mid parry
        enemyObject.transform.Translate(Vector3.right * 2.25f, Space.World);
    }
    protected virtual IEnumerator HitStopController(GameObject enemyObject)
    {
        if (PlayerController.instance.playerRoll.isRolling)
        {
            yield return StartCoroutine(RollingHitStop(enemyObject));
        }
        else
        {
            yield return StartCoroutine(NormalHitStop(enemyObject));
        }
        
    }

    private IEnumerator NormalHitStop(GameObject enemyObject)
    {
        if (!PlayerController.instance.playerZone.inZone && parryCounter == 9) //normal hitstop when entering zone
        {
            //Phase 1: Play sound then stop for a split second while preparing for enemy shake
            AudioManager.instance.Play("CounterSlash");
            ParryShake enemyShaker = enemyObject.GetComponentInChildren<ParryShake>();
            enemiesToShake.Add(enemyShaker); //in case there is a consecutive parry
            HitStop.instance.Stop(0.125f);

            yield return new WaitForSeconds(0.15f);

            //Phase 2: Reposition the enemy and shake them to amplify the effect
            EnemyReposition(enemyObject);
            HitStop.instance.ParryStop(0.5f, enemiesToShake);

            yield return new WaitForSecondsRealtime(0.5f);

            //Phase 3: Setting condition to only play explosion once in case there was more than 1 enemy accessing this function
            if (isRunning) yield break;
            AudioManager.instance.Play("Explosion");
            isRunning = true;
        }
        else if (!consecutiveParry && !PlayerController.instance.playerZone.inZone) //normal hitstop
        {
            AudioManager.instance.Play("CounterSlash");
            HitStop.instance.Stop(0.125f);

            yield return new WaitForSeconds(0.09f);

            EnemyReposition(enemyObject);
        }
        else //Not a hitstop
        {
            yield return StartCoroutine(NoHitStop(enemyObject));
        }
    }

    private IEnumerator NoHitStop(GameObject enemyObject)
    {
        AudioManager.instance.Play("CounterSlash");
        //hit stop shouldn't occur when player is in zone 

        yield return new WaitForSeconds(0.09f);

        EnemyReposition(enemyObject);
    }

    private IEnumerator RollingHitStop(GameObject enemyObject)
    {
        if (!PlayerController.instance.playerZone.inZone)
        {
            //Phase 1: Stop for a split second while preparing for enemy shake
            ParryShake enemyShaker = enemyObject.GetComponentInChildren<ParryShake>();
            enemiesToShake.Add(enemyShaker);
            //add current enemy to a list in case the player is rolling through multiple enemies to shake them all together
            HitStop.instance.Stop(0.125f);

            yield return new WaitForSeconds(0.15f);

            //Phase 2: Shake the enemy to amplify the effect
            HitStop.instance.ParryStop(0.5f, enemiesToShake);

            yield return new WaitForSecondsRealtime(0.5f);

            //Phase 3: Conditions to only play explosion sfx and slash sfx once when transition to zone
            OnParryDashZoneTransition(enemyShaker);
        }
    }

    private void OnParryDashZoneTransition(ParryShake enemyShaker)
    {
        if (10 - parryCounter <= enemiesToShake.Count && enemiesToShake.Last() == enemyShaker)
        {
            AudioManager.instance.Play("CounterSlash");
            AudioManager.instance.Play("Explosion");
        }
        else if (enemiesToShake.First() == enemyShaker) //if not near zone then play the audio normally
        {
            AudioManager.instance.Play("CounterSlash");
        }
    }
}
