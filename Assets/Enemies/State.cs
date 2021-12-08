using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class State
{
    private Action _callback;
    public Action callback{get{return this._callback;} set{this._callback = value;}}
    private String _animid;
    public String animid{get{return this._animid;} set{this._animid = value;}}
    public State(String animid, Action callback)
    {
        this.animid = animid;
        this._callback = () => callback();
    }

    public void ExecuteCallback()
    {
        _callback();
    }
}