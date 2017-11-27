using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFCharacter))]
public class FootMan : MonoBehaviour {

    public static FootMan instance;
    public static FootMan Instance() {
        return instance;
    }

    public VFCharacter m_vfCharcter;
    FootMan()
    {
        instance = this;
    }

	// Use this for initialization
    public Quaternion Dir
    {
        get;
        set;
    }

    public float Speed
    {
        get;
        set;
    }

    void Awake() {
        m_vfCharcter = GetComponent<VFCharacter>();
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            Dir = Quaternion.Euler(0, 90 - 90, 0);
            m_vfCharcter.Move(Dir);
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = Quaternion.Euler(0, 90 - 180, 0);
            m_vfCharcter.Move(Dir);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = Quaternion.Euler(0, 90, 0);
            m_vfCharcter.Move(Dir);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = Quaternion.Euler(0, 90 + 90, 0);
            m_vfCharcter.Move(Dir);
        }
        else {
            //m_vfCharcter.ChangeStatus(RoleState.idle02);
        }
	}
}
