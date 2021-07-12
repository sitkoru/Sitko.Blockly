using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Blockly.Demo.Data.Repositories;

namespace Sitko.Blockly.Demo.Pages
{
    public partial class Show
    {
        [Parameter] public Guid PostId { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            var post = await GetService<PostsRepository>().GetByIdAsync(PostId);
            if (post is null)
            {
                NavigationManager.NavigateTo("/404");
                return;
            }

            Post = post;
        }

        public Post? Post { get; set; }
    }
}
