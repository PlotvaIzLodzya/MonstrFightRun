using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private bool _InitialLevel;

    private WinnerDecider _winnderDecider;
    private PlayerDeathHandler _playerDeathHandler;
    private SaveSystem _saveSystem = new SaveSystem();
    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private float _timePassed;

    public int Counter { get; private set; }

    private void Awake()
    {
        _playerDeathHandler = FindObjectOfType<PlayerDeathHandler>();
        _winnderDecider = FindObjectOfType<WinnerDecider>();


        Counter = _saveSystem.LoadLevelsProgression();
    }

    private void Start()
    {
        _timePassed = Time.time;

        Debug.Log("start " + Counter);
        if (_InitialLevel == false)
            _integrationMetric.OnLevelStart(Counter);
    }

    private void OnEnable()
    {
        if (_playerDeathHandler != null)
            _playerDeathHandler.PlayerLost += OnLevelFailed;

        if (_winnderDecider != null)
            _winnderDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (_playerDeathHandler != null)
            _playerDeathHandler.PlayerLost -= OnLevelFailed;

        if (_winnderDecider != null)
            _winnderDecider.Victory -= OnLevelCompleted;
    }

    public void LoadNextLevel()
    {
        Debug.Log("Level " + Counter);
        if (Counter >= _levelList.SceneCount)
            _levelList.GetRandomScene(Counter).LoadSceneAsync();
        else
            _levelList.GetScene(Counter).LoadSceneAsync();
    }

    public void RestartLevel()
    {
        _integrationMetric.OnRestartLevel(Counter);

        var scene = _levelList.GetCurrentScene();

        Addressables.LoadSceneAsync(scene);
    }

    public void OnLevelCompleted()
    {
        _integrationMetric.OnLevelComplete(GetTime(), Counter);

        Counter++;

        _saveSystem.SaveLevelsProgression(Counter);
    }

    private void OnLevelFailed()
    {
        _integrationMetric.OnLevelFail(GetTime(), Counter);
    }

    private int GetTime()
    {
        return (int)(Time.time - _timePassed);
    }
}
