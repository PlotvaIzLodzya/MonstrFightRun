using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _tutorObjects;
    [SerializeField] private string _saveWord;
    [SerializeField] private bool _pauseTheGame;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(_saveWord))
        {
            CloseTutorial();
        }
        else
        {
            if (_pauseTheGame)
                FreezeGame();
        }

        if(_button != null)
            _button.onClick.AddListener(UnFreezeGame);

        PlayerPrefs.SetString(_saveWord, _saveWord);
    }

    public void FreezeGame()
    {
        if (PlayerPrefs.HasKey(_saveWord))
            return;

        Time.timeScale = 0;
        StartCoroutine(DelayedUnFreeze());
        PlayerPrefs.SetString(_saveWord, _saveWord);
    }

    private void UnFreezeGame()
    {
        Time.timeScale = 1;
        CloseTutorial();
        _button.onClick.RemoveListener(UnFreezeGame);
    }

    private IEnumerator DelayedUnFreeze()
    {
        float elapsedTime = 0;

        while(elapsedTime < 4)
        {
            elapsedTime += Time.unscaledDeltaTime;

            yield return null;
        }

        UnFreezeGame();
    }

    private IEnumerator DelayedFreeze()
    {
        yield return new WaitForSeconds(2);


    }

    private void CloseTutorial()
    {
        foreach (var tutorObject in _tutorObjects)
        {
            tutorObject.SetActive(false);
        }
    }
}
