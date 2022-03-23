using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*10f,0, Input.GetAxis("Vertical")*Time.deltaTime*10f, Space.Self);   
       transform.Rotate(0f,Input.GetAxis("Mouse X")*Time.deltaTime*10f,0f,Space.World);
      transform.Rotate(-Input.GetAxis("Mouse Y")*Time.deltaTime*10f,0f,0f,Space.Self);
    
    if(Input.GetKeyDown(KeyCode.E)){
        GameObject createdBullet = (GameObject)GameObject.Instantiate(Resources.Load("bullet"));
        createdBullet.transform.position= this.transform.position;
        Rigidbody rb= createdBullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*4200f);
    }
    
    if(Input.GetKeyDown (KeyCode.Escape)){
        Application.Quit();
    }
     
    }
}
