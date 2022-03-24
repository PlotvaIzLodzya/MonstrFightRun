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

    public AssetReference GetScene(int index)
    {
        _currentScene = _scenes[index];
        return _currentScene;
    }

    public AssetReference GetCurrentScene()
    {
        if (_currentScene == null)
            _currentScene = _scenes[0];

        return _currentScene;
    }

    public AssetReference GetRandomScene(int currentSceneIndex)
    {
        int index = 0;

        if (SceneCount > 1)
        {
            do
            {
                index = Random.Range(0, _scenes.Length);
            } while (index == currentSceneIndex);
        }

        _currentScene = _scenes[index];
        return _currentScene;
    }
}
