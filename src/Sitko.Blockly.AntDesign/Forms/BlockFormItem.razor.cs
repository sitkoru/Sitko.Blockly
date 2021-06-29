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
    public partial class BlockFormItem
    {
        private static readonly Dictionary<string, object> _noneColAttributes = new Dictionary<string, object>();
        private readonly string _prefixCls = "ant-form-item";

        private bool _isValid;

        private ClassMapper _labelClassMapper = new ClassMapper();
        private string _labelCls = "";
        private EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;

        [Parameter] public RenderFragment LabelTemplate { get; set; }

        [Parameter] public string Label { get; set; }
        [Parameter] public AntLabelAlignType? FormLabelAlign { get; set; } = AntLabelAlignType.Left;

        [Parameter] public bool Required { get; set; } = false;

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public bool NoStyle { get; set; } = false;

        [Parameter] public BlockForm BlockForm { get; set; }
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        [Parameter] public ColLayoutParam? LabelCol { get; set; }

        [Parameter] public ColLayoutParam? WrapperCol { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SetClass();

            _validationStateChangedHandler = (s, e) =>
            {
                var message = CurrentEditContext.GetValidationMessages(BlockForm.FieldIdentifier).Distinct().ToArray();
                _isValid = !message.Any();

                StateHasChanged();
            };
        }

        protected void SetClass()
        {
            this.ClassMapper
                .Add(_prefixCls)
                .If($"{_prefixCls}-with-help {_prefixCls}-has-error", () => _isValid == false)
                .If($"{_prefixCls}-rtl", () => RTL)
                ;

            _labelClassMapper
                .Add($"{_prefixCls}-label")
                .If($"{_prefixCls}-label-left", () => FormLabelAlign == AntLabelAlignType.Left);
        }

        protected override void Dispose(bool disposing)
        {
            if (CurrentEditContext != null && _validationStateChangedHandler != null)
            {
                CurrentEditContext.OnValidationStateChanged -= _validationStateChangedHandler;
            }

            base.Dispose(disposing);
        }

        private string GetLabelClass()
        {
            return Required ? $"{_prefixCls}-required" : _labelCls;
        }

        private Dictionary<string, object> GetLabelColAttributes()
        {
            if (NoStyle)
            {
                return _noneColAttributes;
            }

            ColLayoutParam labelColParameter;

            labelColParameter = LabelCol ?? new ColLayoutParam();

            return labelColParameter.ToAttributes();
        }

        private Dictionary<string, object> GetWrapperColAttributes()
        {
            if (NoStyle)
            {
                return _noneColAttributes;
            }

            ColLayoutParam wrapperColParameter;

            wrapperColParameter = WrapperCol ?? new ColLayoutParam();

            return wrapperColParameter.ToAttributes();
        }
    }
}
