using System;
using System.Collections.Generic;
using System.Linq;
using AntDesign;
using AntDesign.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blazor.Forms;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public partial class AntBlockFormItem
    {
        private static readonly Dictionary<string, object> NoneColAttributes = new();
        private readonly string prefixCls = "ant-form-item";

        private bool isValid;

        private ClassMapper labelClassMapper = new();
        private string labelCls = "";
        private EventHandler<ValidationStateChangedEventArgs>? validationStateChangedHandler;

        [Parameter] public RenderFragment? LabelTemplate { get; set; }

        [Parameter] public string? Label { get; set; }
        [Parameter] public AntLabelAlignType? FormLabelAlign { get; set; } = AntLabelAlignType.Left;

        [Parameter] public bool Required { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; } = null!;

        [Parameter] public bool NoStyle { get; set; }

#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter]
        public BlockForm BlockForm { get; set; } = null!;

        [CascadingParameter] public EditContext? CurrentEditContext { get; set; }

        [Parameter] public ColLayoutParam? LabelCol { get; set; }

        [Parameter] public ColLayoutParam? WrapperCol { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SetClass();

            if (CurrentEditContext is null)
            {
                throw new InvalidOperationException($"{nameof(AntBlockFormItem)} must be used inside EditForm");
            }

            validationStateChangedHandler = (_, _) =>
            {
                var message = CurrentEditContext.GetValidationMessages(BlockForm.FieldIdentifier).Distinct().ToArray();
                isValid = !message.Any();

                StateHasChanged();
            };
        }

        protected void SetClass()
        {
            this.ClassMapper
                .Add(prefixCls)
                .If($"{prefixCls}-with-help {prefixCls}-has-error", () => isValid == false)
                .If($"{prefixCls}-rtl", () => RTL)
                ;

            labelClassMapper
                .Add($"{prefixCls}-label")
                .If($"{prefixCls}-label-left", () => FormLabelAlign == AntLabelAlignType.Left);
        }

        protected override void Dispose(bool disposing)
        {
            if (CurrentEditContext != null && validationStateChangedHandler != null)
            {
                CurrentEditContext.OnValidationStateChanged -= validationStateChangedHandler;
            }

            base.Dispose(disposing);
        }

        private string GetLabelClass() => Required ? $"{prefixCls}-required" : labelCls;

        private Dictionary<string, object> GetLabelColAttributes()
        {
            if (NoStyle)
            {
                return NoneColAttributes;
            }

            ColLayoutParam labelColParameter;

            labelColParameter = LabelCol ?? new ColLayoutParam();

            return labelColParameter.ToAttributes();
        }

        private Dictionary<string, object> GetWrapperColAttributes()
        {
            if (NoStyle)
            {
                return NoneColAttributes;
            }

            ColLayoutParam wrapperColParameter;

            wrapperColParameter = WrapperCol ?? new ColLayoutParam();

            return wrapperColParameter.ToAttributes();
        }
    }
}
