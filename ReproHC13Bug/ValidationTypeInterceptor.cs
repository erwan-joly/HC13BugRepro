using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;
using HotChocolate.Types.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class ValidationTypeInterceptor : TypeInterceptor
{
    public override void OnBeforeCompleteType(
        ITypeCompletionContext completionContext,
        DefinitionBase definition)
    {
        if (definition is InputObjectTypeDefinition inputObjectTypeDefinition)
        {
            foreach (var field in inputObjectTypeDefinition.Fields)
            {
                if (field.Property!.GetCustomAttributes(typeof(ValidationAttribute), true) is
                    ValidationAttribute[] { Length: > 0 } attributes)
                {
                    foreach (var attribute in attributes)
                    {
                        attribute.ErrorMessage = attribute.FormatErrorMessage(
                            field.Property.GetCustomAttribute<DisplayNameAttribute>(false)?.DisplayName ??
                            field.Property.Name);
                        field.AddDirective(attribute, completionContext.TypeInspector);
                    }
                }
            }
        }

        if (definition is ObjectTypeDefinition objectTypeDefinition)
        {
            foreach (var field in objectTypeDefinition.Fields)
            {
                if (field.Member?.GetCustomAttributes(typeof(ValidationAttribute), true) is
                    ValidationAttribute[] { Length: > 0 } attributes)
                {
                    foreach (var attribute in attributes)
                    {
                        attribute.ErrorMessage = attribute.FormatErrorMessage(
                            field.Member.GetCustomAttribute<DisplayNameAttribute>(false)?.DisplayName ??
                            field.Member.Name);
                        field.AddDirective(attribute, completionContext.TypeInspector);
                    }
                }

                foreach (var argument in field.Arguments)
                {
                    if (argument.Parameter?.GetCustomAttributes(typeof(ValidationAttribute), true) is
                        ValidationAttribute[] { Length: > 0 } attributes2)
                    {
                        foreach (var attribute in attributes2)
                        {
                            attribute.ErrorMessage = attribute.FormatErrorMessage(
                                argument.Parameter.GetCustomAttribute<DisplayNameAttribute>(false)?.DisplayName ??
                                argument.Parameter.Name ?? "");
                            field.AddDirective(attribute, completionContext.TypeInspector);
                        }
                    }
                }
            }
        }
    }
}