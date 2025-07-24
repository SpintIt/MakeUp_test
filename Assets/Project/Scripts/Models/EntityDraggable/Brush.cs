using UnityEngine;

public class Brush : EntityDraggable
{
    [field: SerializeField] public Tassel Tassel { get; private set; }
    [field: SerializeField] public MakeUpZoneType MakeUpZoneType { get; private set; }

    public ColorType ColorType { get; private set; }

    public Brush SetColor(ColorType colorType)
    {
        ColorType = colorType;
        return this;
    }
}
