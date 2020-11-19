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
    void Start()
    {
        PickUps = 0;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * -100;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 500;
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
        // transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        if (Input.GetButtonDown("Jump"))
        {
            if (ground == true)
            {

                //transform.Translate(new Vector3(Input.GetAxis(“Horizontal”)*distance, 2, Input.GetAxis(“Vertical”)*distance));
                GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                GetComponent<Rigidbody>().AddForce(Vector3.up * mJumpSpeed);
                ground = false;
                Debug.Log("Jump");
            }
        }
        this.transform.GetComponent<Rigidbody>().AddForce(new Vector3(unitV2.x, 0, unitV2.y) * z * 3);
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
        if (other.transform.CompareTag("Sticky"))
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            distanceToCamera += 0.08f;
            PickUps++;
            SCollider1.radius += 0.02f;
            SCollider2.radius += 0.02f;
            SCollider3.radius += 0.02f;
            SCollider4.radius += 0.02f;
            SCollider5.radius += 0.02f;
            SCollider6.radius += 0.02f;
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
}
