using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MvcTemplate.Components.Mvc
{
    public class FormInputTagHelperTests
    {
        private FormInputTagHelper helper;
        private TagHelperContext context;
        private TagHelperOutput output;

        public FormInputTagHelperTests()
        {
            TagHelperContent content = new DefaultTagHelperContent();
            ModelMetadata metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(String));

            context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<Object, Object>(), "test");
            output = new TagHelperOutput("input", new TagHelperAttributeList(), (_, _) => Task.FromResult(content));
            helper = new FormInputTagHelper { For = new ModelExpression("Test", new ModelExplorer(metadata, metadata, null)) };
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("on", "on")]
        [InlineData(null, null)]
        [InlineData("off", "off")]
        public void Process_Autocomplete(String? value, String? autocomplete)
        {
            output.Attributes.Add("autocomplete", value);

            helper.Process(context, output);

            Assert.Equal(2, output.Attributes.Count);
            Assert.Empty(output.Content.GetContent());
            Assert.Equal("form-control", output.Attributes["class"].Value);
            Assert.Equal(autocomplete, output.Attributes["autocomplete"].Value);
        }

        [Theory]
        [InlineData("", "form-control ")]
        [InlineData(null, "form-control ")]
        [InlineData("test", "form-control test")]
        public void Process_Class(String? value, String classes)
        {
            output.Attributes.Add("class", value);

            helper.Process(context, output);

            Assert.Equal(2, output.Attributes.Count);
            Assert.Empty(output.Content.GetContent());
            Assert.Equal(classes, output.Attributes["class"].Value);
            Assert.Equal("off", output.Attributes["autocomplete"].Value);
        }

        [Fact]
        public void Process_Input()
        {
            helper.Process(context, output);

            Assert.Equal(2, output.Attributes.Count);
            Assert.Empty(output.Content.GetContent());
            Assert.Equal("off", output.Attributes["autocomplete"].Value);
            Assert.Equal("form-control", output.Attributes["class"].Value);
        }
    }
}
