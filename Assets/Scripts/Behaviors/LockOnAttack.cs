using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAttack : SaiMonoBehavior
{
    [SerializeField] MoveLeft movement;
    [SerializeField] GameObject target;
    [SerializeField] float dashSpeed;
    [SerializeField] Transform warning;
    private bool isLockOn;
    private bool isAttacking = false;
    private float levitateSpeed = 1.25f;
    private float rotateSpeed = 500;
    // Start is called before the first frame update
    protected override void LoadComponentsAndValues()
    {
        movement = GetComponent<MoveLeft>();
        target = GameObject.Find("Player");
        warning = transform.parent.Find("Warning");
        dashSpeed = 50;
    }
    private void OnEnable()
    {
        gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 1;
        movement.enabled = true;
        isAttacking = false;
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
        if (transform.parent.position.x > 6) return;

        movement.enabled = false;
        isLockOn = true;
        ObjectFloat(); //spinning object levitation is cool
    }
    protected virtual IEnumerator LockOn(GameObject target)
    {
        isAttacking = true;
        yield return new WaitForSeconds(3f);
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
        warning.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        warning.gameObject.SetActive(false);
    }
    private IEnumerator SetAlert()
    {
        yield return new WaitForSeconds(2.75f);
        StartCoroutine(Alert());
    }
}
