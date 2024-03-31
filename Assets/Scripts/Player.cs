using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PickableObject;
using static Door;

public class Player : MonoBehaviour
{
    private Vector2 movement;
    private InputValue vv;
    private bool pick;
    private Rigidbody rb;
    private bool pickKey = false;
    private bool openDoor = false;
    private bool openNote = false;
    private Collider oth;
    // private bool isRedKey = false;
    // private bool isGreenKey = false;
    // private bool isYellowKey = false;
    private Dictionary<PickableObject.PickableType, bool> keys = new Dictionary<PickableObject.PickableType, bool>();
    [SerializeField] private int health = 100;
    [SerializeField] private float camOffset = -11;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject redText;
    [SerializeField] private GameObject greenText;
    [SerializeField] private GameObject yellowText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private GameObject doorText;
    [SerializeField] private GameObject noteText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject[] notes;

    void Start(){
        camOffset = cam.transform.position.x;
        rb = GetComponent<Rigidbody>();
        EnableText();
    }

    private void OnMovement(InputValue val){
        movement = val.Get<Vector2>();
    }

    private void EnableText(){
        if(keys.ContainsKey(PickableObject.PickableType.keyRed)){
            redText.SetActive(true);
        }
        if(keys.ContainsKey(PickableObject.PickableType.keyGreen)){
            greenText.SetActive(true);
        }
        if(keys.ContainsKey(PickableObject.PickableType.keyYellow)){
            yellowText.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other){
        // UnityEngine.Debug.Log("collision ");
        if(other.gameObject.GetComponent<PickableObject>() != null){
            if(other.gameObject.GetComponent<PickableObject>().PType != PickableType.card){
                oth = other;
                pickKey = true;
                keyText.SetActive(true);
            }
            else {
                pickKey = false;
            }
        }

        if(other.gameObject.GetComponent<Door>() != null){
            oth = other;
            openDoor = true;
            doorText.SetActive(true);
        } else {
            openDoor = false;
        }

        if(other.gameObject.GetComponent<Note>() != null){
            oth = other;
            openNote = true;
            noteText.SetActive(true);
        } else {
            openNote = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        pickKey = false;
        openDoor = false;
        openNote = false;
        keyText.SetActive(false);
        doorText.SetActive(false);
        noteText.SetActive(false);
    }

    // public void OnCollisionEnter(Collider other){
    //     Debug.Log("col");
    //     if(other.gameObject.GetComponent<Door>() != null){
    //         oth = other;
    //         openDoor = true;
    //     } else {
    //         openDoor = false;
    //     }
    // }

    private void OnPick(InputValue val){
        // if(context.performed || context.started)
        //     pick = true;
        // else
        //     pick = false;
        vv = val;
        // Debug.Log("pick " + val);
        if(pickKey){
            keys[oth.gameObject.GetComponent<PickableObject>().PType] = true;
            Destroy(oth.gameObject);
            EnableText();
            pickKey = false;
            keyText.SetActive(false);
        } else if(openDoor){
            if(keys.ContainsKey(oth.gameObject.GetComponent<Door>().keyType)){
                Destroy(oth.gameObject);
                openDoor = false;
                doorText.SetActive(false);
                if(oth.gameObject.GetComponent<Door>().keyType == PickableObject.PickableType.keyYellow){
                    winScreen.SetActive(true);
                }
            }
        } else if(openNote){
            notes[(int)oth.gameObject.GetComponent<Note>().noteNum].SetActive(true);
            // openNote = false;
            // noteText.SetActive(false);
        }
    }

    private void OnTutorial(InputValue val){
        tutorialScreen.SetActive(!tutorialScreen.activeSelf);
    }
    private void OnCloseGame(InputValue val){
        Application.Quit();
    }

    public void CloseNote(GameObject go){
        go.SetActive(false);
    }

    private void DecreaseHealth(int ammount){
        health = health - ammount;
        if(health <= 0){
            //TODO gameover
        }
    }

    void FixedUpdate(){
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, movement.x, 0) * rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 moveDirection = new Vector3(0, 0, movement.y);
        moveDirection = rb.rotation * moveDirection;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        cam.transform.position = new Vector3(rb.position.x + camOffset, cam.transform.position.y, rb.position.z + camOffset);
    }
}
