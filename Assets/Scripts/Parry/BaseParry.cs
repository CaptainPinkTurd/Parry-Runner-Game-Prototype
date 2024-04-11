using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseParry : SaiMonoBehavior
{
    [Header("Parry Mechanic")]
    [SerializeField] internal Collider2D parryCollider;
    [SerializeField] protected float parryForce;
    protected float parryDuration;
    protected Coroutine currentParryState = null;

    [Header("Normal Parry Conditions")]
    internal bool isParry = false;
    internal bool isCounter = false;
    internal bool consecutiveParry;

    [Header("Special Parry Conditions")]
    internal bool isSpecialParry;
    internal bool gotSpecialParried; //check if the enemy got special parried 

    [Header("Parry Related Conditions")]
    internal int parryCounter = 0;
    protected const int enemyLayer = 7;
    protected const int playerLayer = 6;

    protected override void LoadComponentsAndValues()
    {
        //Load all components in script when game start
        LoadParryComponents();
    }
    private void LoadParryComponents()
    {
        parryCollider = GameObject.Find("Parry Window").GetComponentInChildren<Collider2D>();
        parryForce = 60;
        parryDuration = 0.3f;
    }
    internal virtual void CheckForParry()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || SwipeControls.Instance.Tap()) && !isParry)
        {
            currentParryState = StartCoroutine(EnterParryState());
            AudioManager.instance.Play("Poise");
        }
    }
    protected abstract IEnumerator EnterParryState();
    internal abstract IEnumerator Parry(GameObject enemyObject); //initiate parry sequence
    protected abstract void ParryKnockBack(GameObject enemyObject); //force apply when parry
    protected abstract IEnumerator TurnOffParryConditions(GameObject enemyObject); //reset variables when parry end
    protected virtual void EnemyReposition(GameObject enemyObject)
    {
        if (enemyObject.transform.position.x > transform.parent.position.x + 1) return;

        //Reposition the enemy if they phase through the player mid parry
        enemyObject.transform.Translate(Vector3.right * 2.25f, Space.World);
    }
    protected virtual IEnumerator CheckForEnemyDirection(GameObject enemyObject)
    {
        if (enemyObject.transform.position.x > transform.parent.position.x - 0.5f)
        {
            yield return new WaitForSeconds(0.09f);
            yield break;
        }

        PlayerController.instance.model.flipX = true;
        yield return new WaitForSeconds(0.3f);
        PlayerController.instance.model.flipX = false;
    }
}
