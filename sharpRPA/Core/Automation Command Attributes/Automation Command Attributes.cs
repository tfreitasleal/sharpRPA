using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpRPA.Core.AutomationCommands.Attributes.ClassAttributes
{

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class Group : System.Attribute
    {
        public string groupName;
        public Group(string name)
        {
            this.groupName = name;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class Description : System.Attribute
    {
        public string commandFunctionalDescription;
        public Description(string desc)
        {
            this.commandFunctionalDescription = desc;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ImplementationDescription : System.Attribute
    {
        public string commandImplementationDescription;
        public ImplementationDescription(string desc)
        {
            this.commandImplementationDescription = desc;
        }
}

}

namespace sharpRPA.Core.AutomationCommands.Attributes.PropertyAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class PropertyDescription : System.Attribute
    {
        public string propertyDescription;
        public PropertyDescription(string description)
        {
            this.propertyDescription = description;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class PropertyAllowsVariables : System.Attribute
    {
        public bool propertyAllowsVariables;
        public PropertyAllowsVariables(bool allowed)
        {
            this.propertyAllowsVariables = allowed;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertyUISelectionOption : System.Attribute
    {
        public string uiOption;
        public PropertyUISelectionOption(string description)
        {
            this.uiOption = description;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PropertySelectionChangeEvent : System.Attribute
    {
        public string uiOption;
        public PropertySelectionChangeEvent(string description)
        {
            this.uiOption = description;
        }
    }
}

