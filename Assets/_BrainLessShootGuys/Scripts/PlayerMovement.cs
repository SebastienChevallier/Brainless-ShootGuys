using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IHealth
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 aimDirection;
    private PlayerInput playerInput;

    public Transform _weaponAnchor;
    public Transform _visualTranform;
    public PlayerStats _stats;
    public DeviceType _deviceType;
    public LayerMask _layerMask;
    public Camera _camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if(_stats != null)
        {
            _stats.Init();
        }
    }

    private void Update()
    {
        rb.AddForce(moveDirection * _stats._Speed, ForceMode.Impulse);
    }

    public void GetMoveDirection(InputAction.CallbackContext context)
    {
        Vector3 tempVec = context.ReadValue<Vector2>();
        moveDirection = new Vector3(tempVec.x, 0, tempVec.y).normalized;
    }

    public void GetAim(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        Ray aimOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            RaycastHit rayHit;
            if(Physics.Raycast(aimOrigin, out rayHit,20f, _layerMask))
            {
                aimDirection = new Vector3(rayHit.point.x, 0, rayHit.point.z);
            }
        }
        else if(playerInput.currentControlScheme == "Gamepad")
        {
            aimDirection = new Vector3(mousePos.x, 0, mousePos.y).normalized;
        }

        //_visualTranform.rotation = Quaternion.FromToRotation(_visualTranform.transform.up, aimDirection);
        _visualTranform.rotation = Quaternion.LookRotation(aimDirection);
    }

    public void GetShootAction(InputAction.CallbackContext context) 
    {

    }

    public void GetSkillAction(InputAction.CallbackContext context)
    {

    }

    #region equipements
    void Equip()
    {

    }

    void UnEquip() 
    {

    }

    #endregion
}
