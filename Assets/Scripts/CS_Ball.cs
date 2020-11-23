using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ball : MonoBehaviour
{
    public static float timeRemaining;
    public int PickUps;
    public SphereCollider SCollider;
    float x;
    float distanceToCamera = 5;
    public GameObject PickUp;
    public LayerMask m_MagneticLayers;
    public Vector3 m_Position;
    public float mMovespeed = 5f;
    public float mJumpSpeed = 10f;
    public float m_Radius;
    public float m_Force;
    public bool magnetism = false;
    private bool ground = true;
    enum slideVector { nullVector, up, down, left, right };
    private Vector2 touchFirst = Vector2.zero;
    private Vector2 touchSecond = Vector2.zero;
    private slideVector currentVector = slideVector.nullVector;
    private float timer;
    public float offsetTime = 0.1f;
    public float SlidingDistance = 30f;
    private Rigidbody rigidbodyPlayer;

    void Start()
    {
        PickUps = 0;
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1f) * mMovespeed * Time.deltaTime, Space.World);
        if (magnetism)
        {
            Collider[] colliders;
            Rigidbody rigidbody;
            colliders = Physics.OverlapSphere(transform.position + m_Position, m_Radius, m_MagneticLayers);
            foreach (Collider collider in colliders)
            {
                rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
                if (rigidbody == null)
                {
                    continue;
                }
                rigidbody.AddExplosionForce(m_Force * -1, transform.position + m_Position, m_Radius);
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 movement = new Vector3(0f, 0f, 2f);
        Vector3 left = new Vector3(-5f, 0f, 5f);
        Vector3 right = new Vector3(5f, 0f, 5f);
        rigidbodyPlayer.AddForce(movement * 1f);
        if (Input.GetButtonDown("Jump") || currentVector == slideVector.up)
        {
            if (ground == true)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                GetComponent<Rigidbody>().AddForce(Vector3.up * mJumpSpeed);
                ground = false;
                currentVector = slideVector.nullVector;
                Debug.Log("Jump");
            }
            else
            {
                currentVector = slideVector.nullVector;
            }
        }
        timeRemaining += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) || currentVector == slideVector.left)
        {
            if (x == -1)
            {
                x = x;
                currentVector = slideVector.nullVector;
            }
            else
            {
                x--;
                rigidbodyPlayer.AddForce(left * 20f);
                currentVector = slideVector.nullVector;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || currentVector == slideVector.right)
        {
            if (x == 1)
            {
                x = x;
                currentVector = slideVector.nullVector;
            }
            else
            {
                x++;
                rigidbodyPlayer.AddForce(right * 20f);
                currentVector = slideVector.nullVector;
            }
        }
        if (x == -1)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(-2.4f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), 4f * Time.deltaTime);
        }
        if (x == 0)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), 4f * Time.deltaTime);
        }
        if (x == 1)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(2.4f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z), 4f * Time.deltaTime);
        }
        PickupCategories();
    }

    void PickupCategories()
    {
        for (int i = 0; i < PickUp.transform.childCount; i++)
        {
            PickUp.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Trash"))
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            distanceToCamera += 0.08f;
            PickUps++;
            SCollider.radius += 0.005f;
            other.enabled = false;
            other.transform.SetParent(this.transform);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + m_Position, m_Radius);
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
