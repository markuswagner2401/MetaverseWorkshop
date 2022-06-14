using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class FPMover : MonoBehaviour
{
    Vector2 moveInputVector = new Vector2();

    float upDownInputValue;
    Vector3 moveDirection = new Vector3();
    Vector3 smoothedMoveDirection = new Vector3();
    [SerializeField] float moveSpeedFactor = 1f;
    [SerializeField] float smoothing = 0.01f;

    [SerializeField] Animator animator;

    [SerializeField] Transform fpHeadRotation;
    [SerializeField] float headRotationSpeedFactor = 0.1f;

    [SerializeField] bool clampHeadRotation = true;
    [SerializeField] float maxHeadSideRotation = 80f;
    [SerializeField] float maxHeadUpRotation = 80f;

    [SerializeField] float bodyRotationSpeadFactor = 0.1f;
    [SerializeField] float bodyRotationThreshold = 0.3f;
    [SerializeField] AnimationCurve rotationCurve;
    [SerializeField] float rotationTime;
    [SerializeField] Transform headParallelForward;

    bool rotating = false;

    
    float sideRotation = 0f;
    float upRotation = 0f;
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        moveDirection = (fpHeadRotation.forward * moveInputVector.y) + (fpHeadRotation.right * moveInputVector.x) + (fpHeadRotation.up * upDownInputValue) ;


        smoothedMoveDirection = Vector3.Lerp(smoothedMoveDirection, moveDirection, smoothing);

        // transform.Translate(moveDirection * moveSpeedFactor);
        transform.position += moveDirection * moveSpeedFactor;

        fpHeadRotation.transform.eulerAngles = new Vector3(fpHeadRotation.transform.eulerAngles.x,
                                                    sideRotation,
                                                    fpHeadRotation.transform.eulerAngles.z);

        fpHeadRotation.transform.eulerAngles = new Vector3(upRotation,
                                                    fpHeadRotation.transform.eulerAngles.y,
                                                    fpHeadRotation.transform.eulerAngles.z);

        if(Mathf.Abs(transform.eulerAngles.y - fpHeadRotation.transform.eulerAngles.y) > bodyRotationThreshold)
        {
            
            
            
        }

        if(Vector3.Dot(transform.forward, headParallelForward.forward) < 0.8f)
        {
            if (!rotating)
            {
                rotating = true;
                Rotate(headParallelForward.forward);
            }

        }

        
        

    }

    void Rotate(Vector3 targetForward)
    {
        
        
        StartCoroutine(RotateRoutine(targetForward));
        
    }

   

    IEnumerator RotateRoutine(Vector3 targetForward)
    {
        
        Vector3 startForward = transform.forward;
        float timer = 0f;

        while (timer <= rotationTime)
        {
            timer += Time.deltaTime;
            transform.forward = Vector3.Lerp(startForward, targetForward, rotationCurve.Evaluate(timer / rotationTime) );
            
            yield return null;

        }
    
        rotating = false;
        yield break;
    }

    void OnMove(InputValue value)
    {
        moveInputVector = value.Get<Vector2>();
        

        if (Mathf.Abs(value.Get<Vector2>().x)  > 0f || Mathf.Abs(value.Get<Vector2>().y) > 0f)
        {
            print("is moving");
            animator.SetBool("isMoving",true);

            

            animator.SetFloat("ForwardAxis", moveInputVector.y);
            animator.SetFloat("LeftRightAxis", moveInputVector.x);



        }

        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMoveUpDown(InputValue value)
    {
        upDownInputValue = value.Get<Vector2>().y;
    }

    void OnRotate(InputValue value)
    {
        Vector2 currentMouseInput = new Vector2();
        currentMouseInput = value.Get<Vector2>();

        sideRotation = sideRotation + currentMouseInput.x * headRotationSpeedFactor;

        if(clampHeadRotation)
        {
            sideRotation = Mathf.Clamp(sideRotation, -maxHeadSideRotation, maxHeadSideRotation);

        }
    



        upRotation = upRotation + currentMouseInput.y * - headRotationSpeedFactor;

        if(clampHeadRotation)
        {
            upRotation = Mathf.Clamp(upRotation, -maxHeadSideRotation, maxHeadUpRotation);
        }
        
        



        // fpHead.transform.eulerAngles = new Vector3(fpHead.transform.eulerAngles.x,
        //                                             sideRotation ,
        //                                             fpHead.transform.eulerAngles.z);

        // // fpHead.transform.eulerAngles = new Vector3(fpHead.transform.eulerAngles.x,
        // //                                             fpHead.transform.eulerAngles.y + currentMouseInput.x * rotationSpeedFactor,
        // //                                              fpHead.transform.eulerAngles.z);


        // // fpHead.transform.eulerAngles = new Vector3(Mathf.Clamp(fpHead.transform.eulerAngles.x + currentMouseInput.y * -rotationSpeedFactor, -maxHeadUpRotation, maxHeadUpRotation),
        // //                                  fpHead.transform.eulerAngles.y,
        // //                                  fpHead.transform.eulerAngles.z);

        // fpHead.transform.eulerAngles = new Vector3(fpHead.transform.eulerAngles.x + currentMouseInput.y * -rotationSpeedFactor,
        //                                                         fpHead.transform.eulerAngles.y,
        //                                                         fpHead.transform.eulerAngles.z);

        

        

    }
}
