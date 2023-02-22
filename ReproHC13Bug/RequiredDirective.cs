using System.ComponentModel.DataAnnotations;

namespace ReproHC13Bug
{
    public class CustomAttribute : ValidationAttribute
    {
        public CustomAttribute()
        { }
    }
    public class CustomDirective : DirectiveType<CustomAttribute>
    {
        protected override void Configure(IDirectiveTypeDescriptor<CustomAttribute> descriptor)
        {
            descriptor.Name("custom");
            descriptor.Location(DirectiveLocation.InputFieldDefinition | DirectiveLocation.ArgumentDefinition | DirectiveLocation.FieldDefinition);
            descriptor.Ignore(dt => dt.RequiresValidationContext);
            descriptor.Ignore(dt => dt.ErrorMessageResourceName);
        }
    };
}
