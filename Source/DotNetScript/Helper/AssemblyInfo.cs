using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

namespace DotNetScriptbase.Helper
{
    /// <summary>
    /// http://csharphelper.com/blog/2018/03/get-assembly-information-in-c/
    /// </summary>
    internal class AssemblyInfo
    {
        public string Title = "", Description = "", Company = "",
            Product = "", Copyright = "", Trademark = "",
            AssemblyVersion = "", FileVersion = "", Guid = "",
            NeutralLanguage = "";
        public bool IsComVisible = false;

        public AssemblyInfo() : this(Assembly.GetExecutingAssembly()) { }

        public AssemblyInfo(Assembly assembly)
        {

            // Get values from the assembly.
            AssemblyTitleAttribute titleAttr =
                GetAssemblyAttribute<AssemblyTitleAttribute>(assembly);
            if (titleAttr != null) Title = titleAttr.Title;

            AssemblyDescriptionAttribute assemblyAttr =
                GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly);
            if (assemblyAttr != null) Description =
                assemblyAttr.Description;

            AssemblyCompanyAttribute companyAttr =
                GetAssemblyAttribute<AssemblyCompanyAttribute>(assembly);
            if (companyAttr != null) Company = companyAttr.Company;

            AssemblyProductAttribute productAttr =
                GetAssemblyAttribute<AssemblyProductAttribute>(assembly);
            if (productAttr != null) Product = productAttr.Product;

            AssemblyCopyrightAttribute copyrightAttr =
                GetAssemblyAttribute<AssemblyCopyrightAttribute>(assembly);
            if (copyrightAttr != null) Copyright = copyrightAttr.Copyright;

            AssemblyTrademarkAttribute trademarkAttr =
                GetAssemblyAttribute<AssemblyTrademarkAttribute>(assembly);
            if (trademarkAttr != null) Trademark = trademarkAttr.Trademark;

            AssemblyVersion = assembly.GetName().Version.ToString();

            AssemblyFileVersionAttribute fileVersionAttr =
                GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly);
            if (fileVersionAttr != null) FileVersion =
                fileVersionAttr.Version;

            GuidAttribute guidAttr = GetAssemblyAttribute<GuidAttribute>(assembly);
            if (guidAttr != null) Guid = guidAttr.Value;

            NeutralResourcesLanguageAttribute languageAttr =
                GetAssemblyAttribute<NeutralResourcesLanguageAttribute>(assembly);
            if (languageAttr != null) NeutralLanguage =
                languageAttr.CultureName;

            ComVisibleAttribute comAttr =
                GetAssemblyAttribute<ComVisibleAttribute>(assembly);
            if (comAttr != null) IsComVisible = comAttr.Value;
        }

        public static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), true);

            if ((attributes == null) || (attributes.Length == 0)) return null;

            return (T)attributes[0];
        }
    }
}
