using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnAttack : MonoBehaviour
{
    [SerializeField] MoveLeft movement;
    [SerializeField] GameObject target;
    [SerializeField] float dashSpeed;
    [SerializeField] GameObject warning;
    private bool isLockOn;
    private bool isAttacking = false;
    private bool beginAlert = false;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MoveLeft>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        warning.transform.rotation = Quaternion.identity;
        if(transform.parent.position.x <= 6)
        {
            movement.enabled = false;
            isLockOn = true;
            ObjectFloat();
        }
    }
    private void FixedUpdate()
    {
        if (isLockOn && !isAttacking)
        {
            StartCoroutine(LockOn(target));
            StartCoroutine(SetAlert());
        }
    }
    protected virtual IEnumerator LockOn(GameObject target)
    {
        isAttacking = true;
        yield return new WaitForSeconds(3f);
        beginAlert = true;
        var dir = target.transform.position - transform.parent.position;
        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(dir.normalized * dashSpeed, ForceMode2D.Impulse);
    }
    protected virtual void ObjectFloat()
    {
        gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 0;
        transform.parent.Translate(Vector2.up * 1.25f * Time.deltaTime, Space.World);
        transform.parent.Rotate(Vector3.forward * 500 * Time.deltaTime);
    }
    private IEnumerator Alert()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(false);
    }
    private IEnumerator SetAlert()
    {
        yield return new WaitForSeconds(2.75f);
        StartCoroutine(Alert());
    }
}
