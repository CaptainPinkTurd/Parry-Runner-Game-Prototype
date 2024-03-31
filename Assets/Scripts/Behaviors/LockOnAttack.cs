using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAttack : SaiMonoBehavior
{
    [SerializeField] MoveLeft movement;
    [SerializeField] GameObject target;
    [SerializeField] float dashSpeed;
    [SerializeField] Transform warning;
    [SerializeField] private InCameraDetector inCamera;
    private bool isLockOn;
    private bool isAttacking = false;
    private float levitateSpeed = 1.25f;
    private float rotateSpeed = 500;
    protected override void LoadComponentsAndValues()
    {
        movement = GetComponent<MoveLeft>();
        target = GameObject.Find("Player");
        warning = transform.parent.Find("Warning");
        dashSpeed = 60;
        inCamera = transform.parent.GetComponentInChildren<InCameraDetector>();
    }
    private void OnEnable()
    {
        isLockOn = false; //this is what causing the lock on attack to be so unstable, because i didn't set this condition off
        isAttacking = false;
        gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 1;
        movement.enabled = true;
        warning.transform.localPosition = new Vector3(0f, -0.08f, 0f);
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), true);            
    }

    // Update is called once per frame
    void Update()
    {
        LockOnCondition(); 
    }
    private void FixedUpdate()
    {
        LockOnAttackSequence();
    }
    protected virtual void LockOnAttackSequence()
    {
        if (!isLockOn) return;

        warning.transform.rotation = Quaternion.identity;

        if (isAttacking) return;

        StartCoroutine(LockOn(target));
        StartCoroutine(SetAlert());
    }
    protected virtual void LockOnCondition()
    {
        if (transform.parent.position.x > 8) return;

        movement.enabled = false;
        isLockOn = true;
        ObjectFloat(); //spinning object levitation is cool
    }
    protected virtual IEnumerator LockOn(GameObject target)
    {
        isAttacking = true;
        yield return new WaitForSecondsRealtime(2.75f);
        Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), target.GetComponent<Collider2D>(), false);
        var dir = target.transform.position - transform.parent.position;
        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(dir.normalized * dashSpeed, ForceMode2D.Impulse);
    }
    protected virtual void ObjectFloat()
    {
        gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 0;
        transform.parent.Translate(Vector2.up * levitateSpeed * Time.deltaTime, Space.World);
        transform.parent.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    private IEnumerator Alert()
    {
        if (!inCamera.isOnScreen)
        {
            //Set warning on screen when object is out of screen
            warning.transform.position = new Vector3(8f, transform.parent.position.y, 0f);
        }
        warning.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        warning.gameObject.SetActive(false);
    }
    private IEnumerator SetAlert()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        StartCoroutine(Alert());
    }
}
