using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float xStart = 0f;
    public float xEnd = 5f;
    public float speed = 2f;

    private bool goingToEnd = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (goingToEnd)
        {
            pos.x = Mathf.MoveTowards(pos.x, xEnd, speed * Time.deltaTime);
            if (Mathf.Approximately(pos.x, xEnd))
                goingToEnd = false;
        }
        else
        {
            pos.x = Mathf.MoveTowards(pos.x, xStart, speed * Time.deltaTime);
            if (Mathf.Approximately(pos.x, xStart))
                goingToEnd = true;
        }

        transform.position = pos;
    }
}
