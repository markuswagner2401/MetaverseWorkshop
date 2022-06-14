using UnityEngine;
using UnityEngine.InputSystem;

public class VRFreeMove : MonoBehaviour
{
    [SerializeField] Transform testTransform;
    [SerializeField] InputActionReference leftThrustActionRef;
    [SerializeField] InputActionReference rightThrustActionRef;

    [SerializeField] float speeedFactor;

    [SerializeField] Transform controllerLeft;
    [SerializeField] Transform controllerRight;

    [SerializeField] Transform vrOrigin;

    float thrustLeft;
    float thrustRight;

    Vector3 averageDirection = new Vector3();

    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        thrustLeft = leftThrustActionRef.action.ReadValue<float>();
        thrustRight = rightThrustActionRef.action.ReadValue<float>();
       

        averageDirection = AverageDirection(controllerLeft.forward * thrustLeft, controllerRight.forward * thrustRight);

        vrOrigin.position += (thrustRight + thrustLeft) * averageDirection * speeedFactor;

    }

    private Vector3 AverageDirection(Vector3 dirLeftHand, Vector3 dirRightHand)
    {
        return (dirLeftHand + dirRightHand).normalized;
    }
}