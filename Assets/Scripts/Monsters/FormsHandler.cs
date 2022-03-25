using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormsHandler : MonoBehaviour
{
    private List<Form> _forms;
    private int _counter = 0;

    public Animator CurrentFormAnimator => CurrentForm.FormAnimator;
    public Form CurrentForm { get; private set; }

    public event Action FormChanged;

    private void Awake()
    {
        _forms = GetComponentsInChildren<Form>().ToList();
        Error.CheckOnNull(_forms, nameof(Form));

        if (_forms[0] == null)
            return;

        EnableFirstForm();
    }

    public void SetForm(Form form)
    {
        _forms.Add(form);
        EnableFirstForm();
    }

    public bool TryEnableNextForm()
    {
        _counter++;

        if(_counter > _forms.Count-1)
            return false;

        if (CurrentForm != null)
            CurrentForm.gameObject.SetActive(false);

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

        if (CurrentForm != null)
            CurrentForm.gameObject.SetActive(false);

        SetCurrentForm(_counter);
    }

    public void SetCurrentForm(int index)
    {
        if (index > _forms.Count - 1)
            index = _forms.Count - 1;

        CurrentForm = _forms[index];

        CurrentForm.gameObject.SetActive(true);
    }

    private void EnableFirstForm()
    {
        for (int i = 0; i < _forms.Count; i++)
        {
            if (i == _counter)
                SetCurrentForm(i);
            else
                _forms[i].gameObject.SetActive(false);
        }
    }
}
