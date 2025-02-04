using Microsoft.Extensions.DependencyInjection;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Validation;

namespace Sitko.EditorJS.Blazor;

public class EditorJSBlazor<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
{
    private readonly IServiceCollection serviceCollection;

    public EditorJSBlazor(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
        this.serviceCollection.AddSingleton<IEditorJS<TBlockDescriptor>, EditorJS<TBlockDescriptor>>();
        this.serviceCollection.AddJsonLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });
    }

    public EditorJSBlazor<TBlockDescriptor> AddDescriptors<TAssembly, TDescriptor>(bool withValidators = true)
        where TDescriptor : TBlockDescriptor
    {
        serviceCollection.Scan(s => s.FromAssemblyOf<TAssembly>()
                .AddClasses(c => c.AssignableTo<TDescriptor>()
                    .Where(d => d is { IsAbstract: false, IsClass: true }))
                .AsSelfWithInterfaces().WithSingletonLifetime());

        if (withValidators)
        {
            AddValidators<TAssembly, IBlockValidator>();
        }

        return this;
    }

    public EditorJSBlazor<TBlockDescriptor> AddDescriptors<TAssembly>(bool withValidators = true)
    {
        AddDescriptors<TAssembly, TBlockDescriptor>();
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
                .AsSelfWithInterfaces().WithScopedLifetime());

        return this;
    }
}
