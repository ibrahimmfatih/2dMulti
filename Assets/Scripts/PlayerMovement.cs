using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector3 input;
  
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

           


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach (Collider2D coll in colls)
                {
                    if (coll.TryGetComponent(out Ball ball))
                    {
                      

                        Vector3 dir = coll.transform.position - transform.position;
                        ball.Shoot(dir);
                        break;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed;

        // Sadece top hareketsizken veya sahiplik gerçekten gerekli olduðunda sahipliði talep et
        if (GetComponent<PhotonView>().IsMine)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D coll in colls)
            {
                if (coll.TryGetComponent(out Ball ball))
                {
                    if (!ball.GetComponent<PhotonView>().IsMine && ball.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
                    {
                        ball.GetComponent<PhotonView>().RequestOwnership();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
