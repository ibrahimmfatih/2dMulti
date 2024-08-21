using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector3 input;

    public TextMeshProUGUI playerNameText;  // TMP Text bile�eni

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Kendi oyuncunuz i�in kullan�c� ad�n� ayarla ve t�m oyuncularla payla�
        if (photonView.IsMine)
        {
            photonView.RPC("SetPlayerName", RpcTarget.AllBuffered, PhotonNetwork.NickName);
        }
    }

    [PunRPC]
    public void SetPlayerName(string name)
    {
        playerNameText.text = name;
    }

    private void Update()
    {
        if (photonView.IsMine)
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

        // Sadece top hareketsizken veya sahiplik ger�ekten gerekli oldu�unda sahipli�i talep et
        if (photonView.IsMine)
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
