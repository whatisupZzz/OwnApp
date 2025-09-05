
using BuildingBlocks.CQRS;

namespace BuildingBlocks.Message.Events;

public record InternalCommand : IInternalCommand, ICommand;