using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    Animator anim;

    public Player_Variables p_variables;

    public LayerMask mask;

    private RaycastHit hit;

    Audio_Handler audio_Handler;

    BoxCollider box_Col;

    CapsuleCollider cap_col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        p_variables.can_Press = true;

        audio_Handler = FindObjectOfType<Audio_Handler>();

        anim = transform.GetChild(0).GetComponent<Animator>();

        box_Col = GetComponent<BoxCollider>();

        cap_col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (p_variables.game_Begin)
        {
            Check_For_Grounded();

            Handle_EulerAngle();

            Check_For_Fan();

            Inputs();

            Handle_State();

            Handle_Anim();
        }
    }

    void FixedUpdate()
    {
        if (p_variables.game_Begin || p_variables.end_Game)
            Apply_Gravity();
        else rb.isKinematic = true;
    }

    void Apply_Gravity()
    {
        rb.isKinematic = false;

        if(p_variables.is_Grounded)
        {
            rb.mass = 1;

            rb.angularDrag = 0.05f;

            rb.drag = 0;
        }
        else
        {
            rb.mass = 100;

            rb.angularDrag = 25;

            rb.drag = 0;
        }
    }

    void Check_For_Fan()
    {
        if(Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.left, out hit, p_variables.distance_Left, mask))
        {
            if(hit.collider != null)
            {
                if(hit.transform.tag == "Fan")
                {
                    if(p_variables.is_Grounded)
                    {
                        rb.velocity += Vector3.right / 4 * p_variables.fan_Speed / 4;
                    }
                }
            }
        }
    }

    void Handle_EulerAngle()
    {
        if (hit.collider != null && hit.transform.tag != "Stair" && hit.transform.tag != "Ground")
        {
            transform.rotation = hit.transform.rotation;
            transform.GetChild(0).rotation = hit.transform.rotation;
        }
    }

    void Check_For_Grounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * p_variables.distance, Color.red, 0.1f);

        Debug.DrawRay(transform.position + new Vector3(transform.localScale.x / 10, 0, 0), Vector3.down * p_variables.distance, Color.green, 0.1f);

        Debug.DrawRay(transform.position - new Vector3(transform.localScale.x / 10, 0, 0), Vector3.down * p_variables.distance, Color.blue, 0.1f);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, p_variables.distance, mask)
            || Physics.Raycast(transform.position + new Vector3(transform.localScale.x / 10, 0, 0), Vector3.down, out hit, p_variables.distance, mask)
            || Physics.Raycast(transform.position - new Vector3(transform.localScale.x / 10, 0, 0), Vector3.down, out hit, p_variables.distance, mask))
        {
            if (hit.collider != null)
            {
                if (hit.transform.tag != "Ground")
                {
                    Active_Box_Col();

                    Disable_Cap_Col();

                    Freeze_Rigi();

                    p_variables.is_Grounded = true;

                    p_variables.can_Press = true;

                    p_variables.jump_Twice = false;

                    anim.enabled = false;
                }
                else
                {
                    p_variables.lose = true;

                    p_variables.end_Game = true;

                    p_variables.can_Press = false;

                    p_variables.jump_Twice = false;
                }

                if(hit.transform.tag == "Finish")
                {
                    p_variables.win = true;
                }
            }
        }
        else p_variables.is_Grounded = false;
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0) && p_variables.can_Press && !p_variables.end_Game)
        {
            anim.enabled = true;

            audio_Handler.Play("Flip");

            Active_Box_Col();

            Disable_Cap_Col();

            Freeze_Rigi();

            if (!p_variables.is_Grounded)
            {
                p_variables.jump_Twice = true;

                p_variables.jump = false;
            }
            else
            {
                p_variables.jump_Twice = false;

                p_variables.jump = true;

                p_variables.can_Press = true;
            }

            if (!p_variables.jump_Twice)
            {
                Jump();
            }
            else
            {
                p_variables.can_Press = false;

                Double_Jump();
            }
        }

        /////

        if(p_variables.rotate)
        {
            p_variables.rotate = false;

            if (!p_variables.jump_Twice)
                p_variables.jump = true;
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }

    void Handle_State()
    {
        if (p_variables.is_Grounded)
        {
            anim.enabled = false;
        }
        else anim.enabled = true;

        ///

        if (!p_variables.is_Grounded && !p_variables.jump && !p_variables.jump_Twice)
        {
            Disable_Box_Col();
            Active_Cap_Col();
            UnFreeze_Rigi();
        }

        ///

        if(p_variables.end_Game)
        {
            rb.AddForce(Vector3.right * p_variables.force_Acceleration, ForceMode.Acceleration);

            anim.enabled = false;
        }
    }

    public bool fall;

    public void Active_Box_Col()
    {
        box_Col.enabled = true;
    }

    public void Disable_Box_Col()
    {
        box_Col.enabled = false;
    }

    public void Active_Cap_Col()
    {
        cap_col.enabled = true;
    }

    public void Disable_Cap_Col()
    {
        cap_col.enabled = false;
    }

    public void UnFreeze_Rigi()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Freeze_Rigi()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }


    void Handle_Anim()
    {
        anim.SetBool(p_variables.jump_Str, p_variables.jump);

        anim.SetBool(p_variables.jumpTwice_Str, p_variables.jump_Twice);
    }

    public void Jump()
    {
        rb.velocity = Vector3.up * p_variables.force_Up + Vector3.right * p_variables.force_Right;

        p_variables.rotate = true;
    }

    public void Double_Jump()
    {
        rb.velocity = Vector3.up * p_variables.force_Up + Vector3.right * p_variables.force_Right * 1.2f;

        p_variables.rotate = true;
    }
}
