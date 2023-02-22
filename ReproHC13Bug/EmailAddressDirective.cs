using System.ComponentModel.DataAnnotations;

namespace ReproHC13Bug
{
    public class EmailAddressDirective : DirectiveType<EmailAddressAttribute>
    {
        protected override void Configure(IDirectiveTypeDescriptor<EmailAddressAttribute> descriptor)
        {
            descriptor.Name("emailAddress");
            descriptor.Location(DirectiveLocation.InputFieldDefinition | DirectiveLocation.ArgumentDefinition | DirectiveLocation.FieldDefinition);
            descriptor.Ignore(dt => dt.RequiresValidationContext);
            descriptor.Ignore(dt => dt.ErrorMessageResourceName);
            descriptor.Ignore(dt => dt.CustomDataType);
        }
    };
}
