
using System.Xml.Serialization;

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class AUTORIZACION
{

    private AUTORIZACIONCAF cAFField;

    private string rSASKField;

    private string rSAPUBKField;

    public AUTORIZACIONCAF CAF
    {
        get
        {
            return this.cAFField;
        }
        set
        {
            this.cAFField = value;
        }
    }

    /// <remarks/>
    public string RSASK
    {
        get
        {
            return this.rSASKField;
        }
        set
        {
            this.rSASKField = value;
        }
    }

    /// <remarks/>
    public string RSAPUBK
    {
        get
        {
            return this.rSAPUBKField;
        }
        set
        {
            this.rSAPUBKField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AUTORIZACIONCAF
{

    private AUTORIZACIONCAFDA daField;

    private AUTORIZACIONCAFFRMA fRMAField;

    private decimal versionField;

    /// <remarks/>
    public AUTORIZACIONCAFDA DA
    {
        get
        {
            return this.daField;
        }
        set
        {
            this.daField = value;
        }
    }

    /// <remarks/>
    public AUTORIZACIONCAFFRMA FRMA
    {
        get
        {
            return this.fRMAField;
        }
        set
        {
            this.fRMAField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AUTORIZACIONCAFDA
{

    private string reField;

    private string rsField;

    private int tdField;

    private AUTORIZACIONCAFDARNG rNGField;

    private System.DateTime faField;

    private AUTORIZACIONCAFDARSAPK rSAPKField;

    private ushort iDKField;

    /// <remarks/>
    public string RE
    {
        get
        {
            return this.reField;
        }
        set
        {
            this.reField = value;
        }
    }

    /// <remarks/>
    public string RS
    {
        get
        {
            return this.rsField;
        }
        set
        {
            this.rsField = value;
        }
    }

    /// <remarks/>
    public int TD
    {
        get
        {
            return this.tdField;
        }
        set
        {
            this.tdField = value;
        }
    }

    /// <remarks/>
    public AUTORIZACIONCAFDARNG RNG
    {
        get
        {
            return this.rNGField;
        }
        set
        {
            this.rNGField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime FA
    {
        get
        {
            return this.faField;
        }
        set
        {
            this.faField = value;
        }
    }

    /// <remarks/>
    public AUTORIZACIONCAFDARSAPK RSAPK
    {
        get
        {
            return this.rSAPKField;
        }
        set
        {
            this.rSAPKField = value;
        }
    }

    /// <remarks/>
    public ushort IDK
    {
        get
        {
            return this.iDKField;
        }
        set
        {
            this.iDKField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AUTORIZACIONCAFDARNG
{

    private int dField;

    private int hField;

    /// <remarks/>
    public int D
    {
        get
        {
            return this.dField;
        }
        set
        {
            this.dField = value;
        }
    }

    /// <remarks/>
    public int H
    {
        get
        {
            return this.hField;
        }
        set
        {
            this.hField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AUTORIZACIONCAFDARSAPK
{

    private string mField;

    private string eField;

    /// <remarks/>
    public string M
    {
        get
        {
            return this.mField;
        }
        set
        {
            this.mField = value;
        }
    }

    /// <remarks/>
    public string E
    {
        get
        {
            return this.eField;
        }
        set
        {
            this.eField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AUTORIZACIONCAFFRMA
{

    private string algoritmoField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string algoritmo
    {
        get
        {
            return this.algoritmoField;
        }
        set
        {
            this.algoritmoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}
