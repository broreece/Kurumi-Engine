// Engine.
using Engine.State.States.Battle.Text.Base;

namespace Engine.State.States.Battle.Text.Core;

public sealed class BattleText
{
    private float _duration = 0;

    public required string FontName { get; init; }
    public required string Text { get; init; }

    public required float XLocation { get; init; }
    public required float YLocation { get; init; }

    public required BattleTextType TextType { get; init; }

    public float Timer { get; init; }

    public bool Finished => _duration >= Timer;

    public void Update(float deltaTime)
    {
        _duration += deltaTime;
    }
}