using MediatR;

namespace Eximia.CleanArchitecture.SeedWork
{
    public interface ICommand { }
    public interface ICommand<out TResponse> : ICommand, IRequest<TResponse> { }
}