﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;
using System.IO;

// 
// This source code was auto-generated by xsd, Version=4.6.81.0.
// 

namespace CES.CoreApi.Shared.Providers.Helper.Xsd
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Requirements
    {


        private System.Collections.Generic.List<fieldType> fieldsField = new System.Collections.Generic.List<fieldType>();

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Field")]
        public System.Collections.Generic.List<fieldType> Field
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
            }
        }


    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class fieldType
    {

        private string fieldNameField;

        private string requirementTypeField;
       
         /// <remarks/>
        public string FieldName
        {
            get
            {
                return this.fieldNameField;
            }
            set
            {
                this.fieldNameField = value;
            }
        }

        /// <remarks/>
        public string RequirementType
        {
            get
            {
                return this.requirementTypeField;
            }
            set
            {
                this.requirementTypeField = value;
            }
        }
    
    }

}