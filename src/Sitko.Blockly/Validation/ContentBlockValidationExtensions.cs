using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;

namespace Sitko.Blockly.Validation
{
    public static class ContentBlockValidationExtensions
    {
        public static IRuleBuilderOptions<TModel, ContentBlock> AddBlockValidators<TModel>(
            this IRuleBuilderInitialCollection<TModel, ContentBlock> options,
            IEnumerable<AbstractValidator<ContentBlock>>? additionalValidators = null)
        {
            return options
                .SetInheritanceValidator(validator =>
                {
                    validator
                        .Add(new TextBlockValidator())
                        .Add(new CutBlockValidator())
                        .Add(new FileBlockValidator())
                        .Add(new GalleryBlockValidator())
                        .Add(new IframeBlockValidator())
                        .Add(new PictureBlockValidator())
                        .Add(new QuoteBlockValidator())
                        .Add(new TwitchBlockValidator())
                        .Add(new TwitterBlockValidator())
                        .Add(new YoutubeBlockValidator());
                    if (additionalValidators is not null)
                    {
                        foreach (var additionalValidator in additionalValidators)
                        {
                            validator.Add(additionalValidator);
                        }
                    }
                });
        }
    }

    public abstract class AbstractBlocklyFormValidator<TForm> : AbstractValidator<TForm> where TForm : IBlocklyForm
    {
        public AbstractBlocklyFormValidator()
        {
            RuleForEach(f => f.Blocks).AddBlockValidators(AdditionalValidators);
        }

        protected virtual IEnumerable<AbstractValidator<ContentBlock>>? AdditionalValidators => null;
    }
}
