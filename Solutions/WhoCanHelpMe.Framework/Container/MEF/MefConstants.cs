namespace WhoCanHelpMe.Framework.Container.MEF
{
    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    static class MefConstants
    {
        public const string ControllerNameMetadataName = "controller_name";
        public const string ControllerNamespaceMetadataName = "controller_ns";
        public const string ExportedTypeIdentityMetadataName = "ExportTypeIdentity";
        public static readonly string ControllerContract = AttributedModelServices.GetContractName(typeof(IController));
        public static readonly string ControllerTypeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IController));
    }
}