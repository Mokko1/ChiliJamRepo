using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PogoParent : MonoBehaviour
{
    public Transform playerTransform;

    public PlayerController pc;
    bool rotateRight = false;
    bool rotateLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.transform.position.x, playerTransform.transform.position.y, transform.position.z);
        rotateRight = Input.GetKey(KeyCode.L);
        rotateLeft = Input.GetKey(KeyCode.K);
    }

    private void FixedUpdate()
    {
        //Right rotation
        if(rotateRight)
        {
            transform.Rotate(0, 0, -4);
            if(pc._pogoGrounded)
            {
                pc._rb.AddForce(pc.transform.right * 0.4f, ForceMode2D.Impulse);
            }
        }
        //Left rotation
        if (rotateLeft)
        {
            transform.Rotate(0, 0, 4);
            if (pc._pogoGrounded)
            {
                pc._rb.AddForce(-pc.transform.right * 0.4f, ForceMode2D.Impulse);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        pc._pogoGrounded = true;
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        pc._pogoGrounded = false;

    }
}
