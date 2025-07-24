using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blush : MonoBehaviour, IMakedUpZone
{
    [SerializeField] private MakeUpZoneType _makeUpZone;
    [SerializeField] private List<PropertyColorMakeUp> _colors;

    public void Setup()
        => Reset();

    public bool TryAction(EntityDraggable entityDraggable)
    {
        if (entityDraggable is Brush brush && brush.MakeUpZoneType == _makeUpZone)
        {
            Reset();
            _colors.FirstOrDefault(color => color.Type == brush.ColorType)?.Sprite.Show();
            return true;
        }
        else if (entityDraggable is Pomade pomade && _makeUpZone == MakeUpZoneType.Pomade)
        {
            Reset();
            _colors.FirstOrDefault(color => color.Type == pomade.ColorType)?.Sprite.Show();
            return true;
        }

        return false;
    }

    public void Reset()
        => _colors.ForEach(color => color.Sprite.Hide());
}
