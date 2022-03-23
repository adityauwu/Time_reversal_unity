using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
   [RuntimeInitializeOnLoadMethod]
   static void Init(){
       GameObject instanceGO = new GameObject();
       instanceGO.transform.position = Vector3.zero;
       instanceGO.name = "BlockControl-singleton GO";
       instanceGO.AddComponent<BlockControl>();
   }

Dictionary<GameObject, Rigidbody_rotation_and_positionStack> blocksDictionary =  new Dictionary<GameObject, Rigidbody_rotation_and_positionStack>();
//Dictionary<GameObject, Rigidbody_rotation_and_position>

private class Rigidbody_rotation_and_positionStack{   //will be used to hold all thr data of a given stack
    public Rigidbody rb;
    public Stack<Vector3> position = new Stack<Vector3>();  //store position as the cubes are falling from
    public Stack<Quaternion> rotation= new Stack<Quaternion>();  //same for rotation.put the newer positions on top of older ones

  public Rigidbody_rotation_and_positionStack(Rigidbody r, Vector3 pos, Quaternion rot){
rb= r;
position.Push(pos);
rotation.Push(rot);
  
  
  }


}

void Start(){
    GameObject[] blocks= GameObject.FindGameObjectsWithTag("block item");
foreach(GameObject b in blocks){
   b.GetComponent<Rigidbody>().Sleep();
    blocksDictionary.Add(b, new Rigidbody_rotation_and_positionStack(b.GetComponent<Rigidbody>(), b.transform.position, b.transform.rotation));
}
Debug.Log("Singleton's start has finished and we have populated the dictionary,The Dictionary's length is" + blocksDictionary.Count);

}
//end start
public bool isRecording = true;

void Update(){
    if(Input.GetKeyDown(KeyCode.R)){
        isRecording= !isRecording;
    }
}


void FixedUpdate(){
    switch(isRecording) {
             //run one of two functions based on the isRecording

case true:
Record();
break;

case false:
Rewind();
break;
    
    }
 }


void Record(){
    Debug.Log("we are recording");
    foreach(KeyValuePair<GameObject,Rigidbody_rotation_and_positionStack> kvp in blocksDictionary){
        kvp.Value.position.Push(kvp.Key.transform.position);
        kvp.Value.rotation.Push(kvp.Key.transform.rotation);
     }
}

void Rewind(){
    Debug.Log("we are rewinding");

    foreach(KeyValuePair<GameObject,Rigidbody_rotation_and_positionStack> kvp in blocksDictionary){
kvp.Value.rb.Sleep();
if(kvp.Value.position.Count > 0){
    kvp.Key.transform.position = kvp.Value.position.Pop();
    kvp.Key.transform.rotation = kvp.Value.rotation.Pop();
}


    } //end iterating through the pairs in the dictionary
}




}


