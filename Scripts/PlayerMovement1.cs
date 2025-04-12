using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private JoystickController joystickController;
    private CharacterController characterController;
    Vector3 moveVector;
    [SerializeField] private PlayerAnimatior playeranimator;
    [Header("Settings")]
    [SerializeField] private int moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        moveVector=joystickController.GetMovePosition()*moveSpeed*Time.deltaTime/Screen.width;
        moveVector.z=moveVector.y;//joystik yukarý kaldýrýlýnca biz yukarý deðil ileri gitmesini istiyoruz o yüzden y deðerleri z ye eþit olcak
        moveVector.y = 0;//y yukarý gitmesin diye 
        playeranimator.ManageAnimations(moveVector);
        characterController.Move(moveVector);

    }
}
