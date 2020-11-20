using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ball : MonoBehaviour
{
    public int PickUps;
    public SphereCollider SCollider1;
    public SphereCollider SCollider2;
    public SphereCollider SCollider3;
    public SphereCollider SCollider4;
    public SphereCollider SCollider5;
    public SphereCollider SCollider6;
    public float facingAngle = 0;
    float x = 0;
    float z = 0;
    Vector2 unitV2;
    Vector3 dir = Vector3.zero;
    public GameObject cameraReference;
    float distanceToCamera = 5;
    public GameObject PickUp;
    public LayerMask m_MagneticLayers;
    public Vector3 m_Position;
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
    public float SlidingDistance = 80f;
    void Start()
    {
        PickUps = 0;
    }

    void Update()
    {
        // dir.x = -Input.acceleration.y;
        // dir.z = Input.acceleration.x;
        // if (dir.sqrMagnitude > 1)
        // {
        //     dir.Normalize();
        // }
        // dir *= Time.deltaTime;
        
        x = Input.GetAxis("Horizontal") * Time.deltaTime * -100;
        x = Input.acceleration.x * Time.deltaTime * -100;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 500;
        z = Input.acceleration.y * Time.deltaTime * 500;
        facingAngle += x;
        unitV2 = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));

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
        this.transform.GetComponent<Rigidbody>().AddForce(new Vector3(unitV2.x, 0, unitV2.y) * z * 3);
        // this.transform.GetComponent<Rigidbody>().AddForce(dir*5);
        cameraReference.transform.position = new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;
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
            SCollider1.radius += 0.01f;
            SCollider2.radius += 0.01f;
            SCollider3.radius += 0.01f;
            SCollider4.radius += 0.01f;
            SCollider5.radius += 0.01f;
            SCollider6.radius += 0.01f;
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
