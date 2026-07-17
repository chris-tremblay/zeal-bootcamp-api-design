using Zeal.Bootcamp.DnD.Domain.Core;

namespace Zeal.Bootcamp.DnD.Domain.Characters.Events;

public sealed class CharacterCreated(Character source) : DomainEvent<Character>(source);
