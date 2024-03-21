using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private float rotationDegree = 180;
    public float currentRotation = 45;
    // Start is called before the first frame update
    void Start()
    {
        rotationDegree = 180 - currentRotation * 2;
        StartCoroutine(ChangeRotation());
    }

    // Update is called once per frame
    void Update()
    {
        Zigzag();
    }
    protected virtual void Zigzag()
    {
        transform.parent.Translate(Vector2.up * speed * Time.deltaTime);
    }
    protected virtual IEnumerator ChangeRotation()
    {
        while (true)
        {
            currentRotation += rotationDegree;
            Quaternion rotation = Quaternion.Euler(0f, 0f, currentRotation);
            transform.parent.rotation = rotation;
            rotationDegree *= -1;
            yield return new WaitForSeconds(1);
        }
    }
}
