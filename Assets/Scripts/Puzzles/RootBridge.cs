using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBridge : MonoBehaviour
{
    public enum RootState { sprout, grown, withered }
    public RootState curState;

    private Dictionary<RootState, Action> States = new Dictionary<RootState, Action>();

    [SerializeField] private LayerMask _mask;

    public bool _hasSun = true;
    public bool _iswatered;

    [SerializeField] private GameObject _witherForm, _sproutForm, _grownForm;

    private void Start()
    {
        States.Add(RootState.sprout, State_Sprout);
        States.Add(RootState.grown, State_Grown);
        States.Add(RootState.withered, State_Withered);
	}

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 100, _mask))
        {
            _hasSun = true;
            States[curState]?.Invoke();
        }
        else
        {
            _hasSun = false;
            States[curState]?.Invoke();
        }
    }

    public void AttemptToWater()
    {
        _iswatered = true;
        States[curState]?.Invoke();
    }

    void State_Sprout()
    {
        if (_iswatered)
        {
            _sproutForm.SetActive(false);
            _grownForm.SetActive(true);
            curState = RootState.grown;
        }

        if (!_hasSun)
        {
            _iswatered = false;
            _sproutForm.SetActive(false);
            _witherForm.SetActive(true);
            curState = RootState.withered;
        }
    }

    void State_Grown()
    {
        if (!_hasSun)
        {
            _iswatered = false;
            _grownForm.SetActive(false);
            _witherForm.SetActive(true);
            curState = RootState.withered;
        }
    }

    void State_Withered()
    {
        if (_hasSun)
        {
            _witherForm.SetActive(false);
            _sproutForm.SetActive(true);
            curState = RootState.sprout;
        }
    }
}
