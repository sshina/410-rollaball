using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    //https://stackoverflow.com/questions/58377170/how-to-jump-in-unity-3d
    private int jumpcount = 0;
    public float jumpforce = 0.5f;
    public Vector3 jumpvect = new Vector3(0.0f, 0.5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent <Rigidbody>(); 
        count = 0; 
        SetCountText();
    }
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
           other.gameObject.SetActive(false);
           count = count + 1;
           SetCountText();
        }
    }
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;  
    }
    void OnCollisionStay(){
        jumpcount = 0;
    }
    void SetCountText() 
    {
       countText.text =  "Count: " + count.ToString();
        if (count >= 8)
        {
           winTextObject.SetActive(true);
        }
    }
    //from linked stack post
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && (jumpcount < 1)){
            rb.AddForce(jumpvect * jumpforce, ForceMode.Impulse);
            jumpcount++;
        }
    }
}
