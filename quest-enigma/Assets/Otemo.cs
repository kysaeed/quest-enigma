using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otemo : MonoBehaviour
{
    protected int _data = 0;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    public void onHit()
    {
        this._animator.SetBool("isHit", true);
        this._animator.SetBool("isFailed", false);
    }

    public void onFailed()
    {
        this._animator.SetBool("isHit", false);
        this._animator.SetBool("isFailed", true);
    }


    public void setData(int data)
    {
        this._data = data;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 p = Camera.main.transform.position;
        //p.y = transform.position.y;
        //transform.LookAt(p);

    }
}
