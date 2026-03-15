namespace Save.Interfaces;

/// <summary>
/// Interface used to represent saveable characters, contains stats, skills, statuses and an ID.
/// </summary>
public interface ISaveableCharacter : ICharacterModifiersAccessor, IStatusAccessor {}