using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Tassel : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetColor(Color color)
        => _image.color = color;
}