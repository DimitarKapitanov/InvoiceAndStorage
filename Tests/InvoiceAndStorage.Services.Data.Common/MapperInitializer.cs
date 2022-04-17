namespace InvoiceAndStorage.Services.Data.Common
{
    using System.Reflection;
    using InvoiceAndStorage.Services.Data;
    using InvoiceAndStorage.Services.Mapping;
    using InvoiceAndStorage.Web.ViewModels.Buyers;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CompanyService).GetTypeInfo().Assembly,
                typeof(BuyerService).GetTypeInfo().Assembly,
                typeof(DataBaseOwnerService).GetTypeInfo().Assembly,
                typeof(AddBuyerViewModel).GetTypeInfo().Assembly);
        }
    }
}
