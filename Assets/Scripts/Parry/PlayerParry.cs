using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : BaseParry
{
    protected override IEnumerator EnterParryState()
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
    internal override IEnumerator Parry(GameObject enemyObject)
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

        //Phase 4: setting up conditions upon exiting parry 
        StartCoroutine(TurnOffParryConditions(enemyObject));
        GameManager.instance.score += 50;
        parryCounter++;
    }
    protected override IEnumerator TurnOffParryConditions(GameObject enemyObject)
    {
        isParry = false;
        isCounter = false;
        StopCoroutine(currentParryState); //stop the ongoing function of parry state, it will mess with the flow of the player functionality if left untouch 
        parryCollider.enabled = false;

        yield return new WaitForSecondsRealtime(0.1f);
        enemyObject.layer = playerLayer; //turn enemy into player's projectile after deflect them
        CheckIfConsecutiveParry();
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false; //turn on player vulnerability again
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
        if (!isParry) return;
        consecutiveParry = true;
    }
}
