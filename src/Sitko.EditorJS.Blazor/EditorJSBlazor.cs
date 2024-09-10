using Microsoft.Extensions.DependencyInjection;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blazor.Validation;

namespace Sitko.EditorJS.Blazor;

public class EditorJSBlazor<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
{
    private readonly IServiceCollection serviceCollection;

    public EditorJSBlazor(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
        this.serviceCollection.AddSingleton<IEditorJS<TBlockDescriptor>, EditorJS<TBlockDescriptor>>();
        this.serviceCollection.AddLocalization();
    }

    public EditorJSBlazor<TBlockDescriptor> AddBlocks<TAssembly, TDescriptor>(bool withValidators = true)
        where TDescriptor : TBlockDescriptor
    {
        serviceCollection.Scan(s => s.FromAssemblyOf<TAssembly>()
                .AddClasses(c => c.AssignableTo<TDescriptor>().Where(d => !d.IsAbstract && d.IsClass))
                .AsSelfWithInterfaces().WithSingletonLifetime());

        if (withValidators)
        {
            AddValidators<TAssembly, IBlockValidator>();
        }

        return this;
    }

    public EditorJSBlazor<TBlockDescriptor> AddBlocks<TAssembly>(bool withValidators = true)
    {
        AddBlocks<TAssembly, TBlockDescriptor>();
        if (withValidators)
        {
            AddValidators<TAssembly, IBlockValidator>();
        }

        return this;
    }

    public EditorJSBlazor<TBlockDescriptor> AddValidators<TAssembly, TValidator>()
        where TValidator : IBlockValidator
    {
        serviceCollection.Scan(s => s
                .FromAssemblyOf<TAssembly>().AddClasses(c => c.AssignableTo<TValidator>())
                .FromAssemblyOf<EditorJS>().AddClasses(c => c.AssignableTo<TValidator>())
                .AsSelfWithInterfaces().WithScopedLifetime());

        return this;
    }
}
