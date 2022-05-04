using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPicture : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private Image _image;

    public void Init(Vector3 destination, Sprite sprite, float size, WalletView walletView, float amount)
    {
        _image.sprite = sprite;
        StartCoroutine(Fly(destination, walletView, amount));
        StartCoroutine(SizeChange(size));
    }

    private IEnumerator Fly(Vector3 destination, WalletView walletView, float amount)
    {
        float distance = Vector3.Distance(transform.localPosition, destination);
        float speed = distance / _time; 

        while(distance > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);

            distance = Vector3.Distance(transform.localPosition, destination);
            yield return null;
        }

        walletView.ChangeViewText(amount);
        Destroy(gameObject);
    }

    private IEnumerator SizeChange(float size)
    {
        float speed = Mathf.Abs(_image.rectTransform.sizeDelta.x - size)/_time;
        float currentSize;

        while (_image.rectTransform.sizeDelta.x > size)
        {
            currentSize = Mathf.MoveTowards(_image.rectTransform.sizeDelta.x, size, speed * Time.deltaTime);
            _image.rectTransform.sizeDelta = new Vector2(currentSize, currentSize);

            yield return null;
        }
    }
}
