using UnityEngine;
using System.Collections;

public class BodyPartCON : MonoBehaviour {

    //Hinge Varables

    public JState State;

    public GameObject MFree;
    public GameObject MLock;
    public GameObject MExtend;
    public GameObject MContract;

    public GameObject AttachmentPoint;

    HingeJoint Hinge;
    JointMotor Motor;
    Rigidbody Rb;

    public float DefaultJointForce = 10;
    public float JointForce;
    public float DefaultJointVelocity = 10;
    public float JointVelocity;


    // Use this for initialization
    void Start () {
        Rb = GetComponent<Rigidbody>();
        Hinge = GetComponent<HingeJoint>();
        Motor = Hinge.motor;
        SetState(JState.Lock);
        JointVis(State);
        JointForce = DefaultJointForce;
        JointVelocity = DefaultJointVelocity;
        Rb.mass = 0;
    }

    void OnMouseDown()
    {
        CycleState();
        JointVis(State);
    }

    //State Management
    void SetState(JState _in)
    {
        switch (_in)
        {
            case JState.Lock:
                JointLock();
                break;
            case JState.Free:
                JointFree();
                break;
            case JState.Contract:
                JointContract();
                break;
            case JState.Extend:
                JointExtend();
                break;
        }
    }
    void CycleState()
    {
        switch (State)
        {
            case JState.Lock:
                JointFree();
                break;
            case JState.Free:
                JointContract();
                break;
            case JState.Contract:
                JointExtend();
                break;
            case JState.Extend:
                JointLock();
                break;
        }
    }

    //Joint Management
    void JointVis(JState _in)
    {
        switch (_in)
        {
            case JState.Lock:
                MLock.SetActive(true);
                MExtend.SetActive(false);
                MContract.SetActive(false);
                break;
            case JState.Free:
                MLock.SetActive(false);
                MExtend.SetActive(false);
                MContract.SetActive(false);
                break;
            case JState.Contract:
                MLock.SetActive(false);
                MExtend.SetActive(false);
                MContract.SetActive(true);
                break;
            case JState.Extend:
                MLock.SetActive(false);
                MExtend.SetActive(true);
                MContract.SetActive(false);
                break;
        }
    }
    void JointLock()
    {
        State = JState.Lock;
        UpdateJoint(JointForce, 0);
        print("Lock" + gameObject.name);
    }
    void JointFree()
    {
        State = JState.Free;
        UpdateJoint(0, 0);
        print("Free" + gameObject.name);
    }
    void JointContract()
    {
        State = JState.Contract;
        UpdateJoint(JointForce, (JointVelocity * -1));
        print("Contract" + gameObject.name);
    }
    void JointExtend()
    {
        State = JState.Extend;
        UpdateJoint(JointForce, JointVelocity);
        print("Extend" + gameObject.name);
    }
    void UpdateJoint(float _inForce, float _inVelocity)
    {
        Motor.force = _inForce * 10;
        Motor.targetVelocity = _inVelocity;
        Hinge.motor = Motor;
    }
}

public enum JState { Lock, Free, Contract, Extend};
