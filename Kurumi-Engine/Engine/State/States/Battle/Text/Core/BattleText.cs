// Engine.
using Engine.State.States.Battle.Text.Base;

namespace Engine.State.States.Battle.Text.Core;

public sealed class BattleText
{
    private readonly float _timer;

    private float _duration = 0;

    public required string FontName { get; init; }
    public required string Text { get; init; }

    public required uint FontSize { get; init; }

    public required BattleTextType TextType { get; init; }

    public bool Finished => _duration >= _timer;

    internal BattleText(float timer)
    {
        _timer = timer;
    }

    public void Update(float deltaTime)
    {
        _duration += deltaTime;
    }
}