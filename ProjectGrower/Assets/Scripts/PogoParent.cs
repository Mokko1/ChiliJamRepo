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
    bool isChargingJump = false;
    float maxPogoChargeTime = 2.5f;
    public GameObject pogoShaft;
    public Collider2D Collider2D;
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
        isChargingJump = Input.GetKey(KeyCode.Space);
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
        if(isChargingJump)
        {
            StartCoroutine(chargeJump());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        pc._pogoGrounded = true;

        //If collision angle equals something that indicates its straight to the ground, enable jumping logic

        
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        pc._pogoGrounded = false;

    }

    public IEnumerator chargeJump()
    {
        //If grounded, start charging jump
        //each frame keep track of time jump is being charged, up to max charge time
        //Also make sure to shorten the collider to simulate pogo charging

        float timeCharged = 0.2f;

        while (Input.GetKey(KeyCode.Space) && pc._pogoGrounded)
        {
            timeCharged += Time.deltaTime;
            pogoShaft.transform.localPosition = new Vector3(1.70741f - Mathf.Min(timeCharged, maxPogoChargeTime) * 0.2f, 0, 0);
            
            yield return null;
        }

        timeCharged = Mathf.Min(timeCharged, maxPogoChargeTime);
        releaseJump(timeCharged);
    }

    public void releaseJump(float timeCharged)
    {
        pc._rb.AddForce(Vector2.up * timeCharged * 0.1f, ForceMode2D.Impulse);
        pogoShaft.transform.localPosition = new Vector3(1.70741f, 0, 0);
        pc._pogoJumping = true;
    }
}
