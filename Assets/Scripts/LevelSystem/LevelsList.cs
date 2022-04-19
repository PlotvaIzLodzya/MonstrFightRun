using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scriptable Objects/Levels list")]
public class LevelsList : ScriptableObject
{
    [SerializeField] private AssetReference[] _scenes;

    private AssetReference _currentScene;
    public int SceneCount => _scenes.Length-1;

    private const string CurrentLevelIndex = "CurrentLevelIndex";

    public AssetReference GetScene(int index)
    {
        _currentScene = _scenes[index];

        SaveCurrentIndex(index);

        return _currentScene;
    }

    public AssetReference GetCurrentScene()
    {
        if (_currentScene == null)
        {
            if (PlayerPrefs.HasKey(CurrentLevelIndex))
                _currentScene = _scenes[PlayerPrefs.GetInt(CurrentLevelIndex)];
            else
                _currentScene = _scenes[0];

            Debug.Log(PlayerPrefs.GetInt(CurrentLevelIndex));
        }
            

        return _currentScene;
    }

    public AssetReference GetRandomScene(int counter)
    {
        int index = 0;

        if (counter % 5 == 0)
        {
            index = 5;

            SaveCurrentIndex(index);

            return _scenes[index];
        }

        if (SceneCount > 1)
        {
            do
            {
                index = Random.Range(0, _scenes.Length);
            } while (index == counter);
        }

        _currentScene = _scenes[index];

        SaveCurrentIndex(index);

        return _currentScene;
    }

    private void SaveCurrentIndex(int index)
    {
        PlayerPrefs.SetInt(CurrentLevelIndex, index);
    }
}
