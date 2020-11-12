using Autofac;
using Eximia.CleanArchitecture.WebAPI.Infraestrutura.MediatRBehaviors;
using MediatR;
using System.Reflection;
using Eximia.CleanArchitecture.Avaliacoes;

namespace Eximia.CleanArchitecture.WebAPI.Infraestrutura.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Registrar todos os handlers para comandos de um assembly
            builder.RegisterAssemblyTypes(typeof(Ambiente).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(EnrichLogContextBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();
        }
    }
}
