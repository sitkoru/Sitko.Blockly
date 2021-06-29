using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.Blazor;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.AntDesignComponents
{
    public class AntDesignBlocklyModuleConfig : BlocklyModuleConfig<IBlazorBlockDescriptor>
    {
        public CKEditorTheme CKEditorTheme { get; set; } = CKEditorTheme.Light;

        public readonly List<Action<IServiceCollection>> ConfigureServicesActions = new();

        public AntDesignBlocklyModuleConfig ConfigureDefaultStorage<TBlockStorageOptions>()
            where TBlockStorageOptions : class, IBlockStorageOptions
        {
            ConfigureServicesActions.Add(services =>
            {
                services.AddSingleton<IBlockStorageOptions, TBlockStorageOptions>();
            });
            return this;
        }

        public AntDesignBlocklyModuleConfig ConfigureFormStorage<TForm, TBlockFormStorageOptions>()
            where TForm : BaseForm, IBlocklyForm
            where TBlockFormStorageOptions : class, IBlockFormStorageOptions<TForm>
        {
            ConfigureServicesActions.Add(services =>
            {
                services.AddSingleton<IBlockFormStorageOptions<TForm>, TBlockFormStorageOptions>();
            });
            return this;
        }
    }
}
