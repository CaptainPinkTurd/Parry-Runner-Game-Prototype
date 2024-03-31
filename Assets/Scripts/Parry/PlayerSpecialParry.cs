using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialParry : BaseParry
{
    protected override IEnumerator EnterParryState()
    {
        throw new System.NotImplementedException();
    }
    internal override IEnumerator Parry(GameObject enemyObject)
    {
        if (enemyObject.layer != enemyLayer || PlayerController.instance.playerDeath.isDead) yield break;

        //Phase 1: setting up player conditions for parry
        PlayerController.instance.playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        PlayerController.instance.playerCollision.allowCollision = true;

        //Phase 2: initiating the attack animation
        isSpecialParry = true;

        yield return new WaitForSeconds(0.09f);

        //Phase 3: obliterating the enemy
        ParryKnockBack(enemyObject);

        //Phase 4: setting up conditions upon exiting parry 
        StartCoroutine(TurnOffParryConditions(enemyObject));
    }
    protected override void ParryKnockBack(GameObject enemyObject)
    {
        //Phase 1: Setting Up Variables
        Collider2D groundCollider = GameObject.Find("Ground").GetComponent<Collider2D>();
        Collider2D enemyCollider = enemyObject.GetComponent<Collider2D>();
        var targetEnemy = FindClosestTarget.Instance.FindClosestEnemy(enemyObject.transform);
        Rigidbody2D enemyRb = enemyObject.GetComponent<Rigidbody2D>();
        Vector2 enemyDir = targetEnemy.transform.position - transform.parent.position;

        //Phase 2: Adjusting Collision
        Physics2D.IgnoreCollision(enemyCollider, groundCollider); //ignore ground to make the explosion more stylish
        enemyRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //prevent object from phasing

        //Phase 3: Adding Knockback Forces
        enemyRb.velocity = Vector2.zero; //reset velocity to avoid unwanted deflect direction
        enemyRb.AddForce(enemyDir.normalized * parryForce, ForceMode2D.Impulse);
        enemyRb.AddTorque(parryForce, ForceMode2D.Impulse);
        gotSpecialParried = true;
    }
    protected override IEnumerator TurnOffParryConditions(GameObject enemyObject)
    {
        enemyObject.layer = playerLayer; //turn on layer immediately to ensure the explosion collision

        yield return new WaitForSeconds(0.1f);
        isSpecialParry = false;
        PlayerController.instance.playerRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //disable freeze pos at y
        PlayerController.instance.playerCollision.allowCollision = false; //turn on player vulnerability again
    }
}
