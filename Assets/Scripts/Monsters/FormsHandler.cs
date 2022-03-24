using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormsHandler : MonoBehaviour
{
    private bool NOICOn;
    private Form[] _forms;
    private int _counter = 0;
    private Form _currentForm;

    public Animator CurrentFormAnimator => _currentForm.FormAnimator;

    public event Action FormChanged;

    private void Awake()
    {
        _forms = GetComponentsInChildren<Form>();
        Error.CheckOnNull(_forms, nameof(Form));

        for (int i = 0; i < _forms.Length; i++)
        {
            if (i == _counter)
                SetCurrentForm(i);
            else
                _forms[i].gameObject.SetActive(false);
        }
    }

    public bool TryEnableNextForm()
    {
        _counter++;

        if(_counter > _forms.Length-1)
            return false;

        if (_currentForm != null)
            _currentForm.gameObject.SetActive(false);

        SetCurrentForm(_counter);
        FormChanged?.Invoke();
        return true;
    }

    public void EnablePreviousForm()
    {
        _counter--;

        if (_counter <= 0)
        {
            _counter = 0;
            return;
        }

        if (_currentForm != null)
            _currentForm.gameObject.SetActive(false);

        SetCurrentForm(_counter);
    }

    public void SetCurrentForm(int index)
    {
        if (index > _forms.Length - 1)
            index = _forms.Length - 1;

        _currentForm = _forms[index];

        _currentForm.gameObject.SetActive(true);
    }
}
