using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float hitForce;
    public float moveSpeed;
    public float rotSpeed;
    private Rigidbody myRigidBody;
    public AudioClip[] clips;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float totalCharge = 1f;

    private Camera mainCamera;
   public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("SampleScene");
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 4)
        {
            audioSource.volume = collision.relativeVelocity.magnitude;
            audioSource.PlayOneShot(clips[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        // moveVelocity = moveInput.normalized * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 dir = pointToLook - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotSpeed * Time.time);
            //transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, pointToLook.z));

            Vector3 mouseWorldPosition = pointToLook;

            //Angle between mouse and this object
            float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

            //Ta daa
            //transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }

        if (transform.position.y < 1 && myRigidBody.velocity.magnitude < 5)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                
                    audioSource.Play();
                
                if (totalCharge < 50)
                {
                    totalCharge += Time.deltaTime * 25;
                    
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                audioSource.Stop();
                Debug.Log(totalCharge);
                myRigidBody.AddRelativeForce(hitForce * totalCharge * Vector3.forward);
                totalCharge = 1;
                audioSource.PlayOneShot(clips[0]);
            }
        }  else
        {
            totalCharge = 1;
        }
        ChargeBar.SetHealthBarValue(totalCharge / 50);


    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}
//    private void Update()
//    {
//        // myRigidBody.velocity = moveVelocity;
        

//        if (Input.GetKey(KeyCode.Space))
//        {
//            if (totalCharge < 50)
//            {
//                totalCharge += Time.fixedTime * 20;
//            }
//        }
//        if (Input.GetKeyUp(KeyCode.Space))
//        {
//            Debug.Log(totalCharge);
//            myRigidBody.AddRelativeForce(hitForce * totalCharge * Vector3.forward);
//            totalCharge = 1;
//        }

//    }
//}
