using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    public float padSpeed = 0.5f;
    public float moveDistance = 10f;

    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(transform.position.x, 1f, -3f);
        endPos = startPos + new Vector3(moveDistance, 0f, 0f);
        StartCoroutine(MovePad());
    }

    IEnumerator MovePad()
    {
        Vector3 start = startPos;
        Vector3 end = endPos;

        while (true)
        {
            float term = 0f;

            while(term < 1f)
            {
                term += Time.deltaTime * padSpeed;
                transform.position = Vector3.Lerp(start, end, term);
                yield return null;
            }

            Vector3 temp = start;
            start = end;
            end = temp;
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.transform.SetParent(this.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);        
    }
}
