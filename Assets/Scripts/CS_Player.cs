using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    enum slideVector { nullVector, up, down, left, right };
    private Vector2 touchFirst = Vector2.zero;
    private Vector2 touchSecond = Vector2.zero;
    private slideVector currentVector = slideVector.nullVector;
    private float timer;
    public float offsetTime = 0.1f;
    public float SlidingDistance = 80f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)

        {
            touchFirst = Event.current.mousePosition;
        }
        if (Event.current.type == EventType.MouseDrag)

        {
            touchSecond = Event.current.mousePosition;

            timer += Time.deltaTime;

            if (timer > offsetTime)
            {
                touchSecond = Event.current.mousePosition;
                Vector2 slideDirection = touchFirst - touchSecond;
                float x = slideDirection.x;
                float y = slideDirection.y;

                if (y + SlidingDistance < x && y > -x - SlidingDistance)
                {
                    if (currentVector == slideVector.left)
                    {
                        return;
                    }
                    Debug.Log("right");
                    currentVector = slideVector.left;
                }
                else if (y > x + SlidingDistance && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.right)
                    {
                        return;
                    }

                    Debug.Log("left");
                    currentVector = slideVector.right;
                }
                else if (y > x + SlidingDistance && y - SlidingDistance > -x)
                {
                    if (currentVector == slideVector.up)
                    {
                        return;
                    }

                    Debug.Log("up");
                    currentVector = slideVector.up;
                }
                else if (y + SlidingDistance < x && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.down)
                    {
                        return;
                    }

                    Debug.Log("Down");
                    currentVector = slideVector.down;
                }

                timer = 0;
                touchFirst = touchSecond;
            }
            if (Event.current.type == EventType.MouseUp)
            {
                currentVector = slideVector.nullVector;
            }
        }
    }
}
