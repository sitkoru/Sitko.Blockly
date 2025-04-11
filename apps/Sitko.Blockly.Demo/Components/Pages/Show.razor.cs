using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Data.Entities;
using Sitko.Core.Repository;

namespace Sitko.Blockly.Demo.Components.Pages;

public partial class Show
{
    [Parameter] public Guid PostId { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        var post = await GetRequiredService<IRepository<Post, Guid>>().GetByIdAsync(PostId);
        if (post is null)
        {
            NavigationManager.NavigateTo("/404");
            return;
        }

        Post = post;
    }

    public Post? Post { get; set; }
}
