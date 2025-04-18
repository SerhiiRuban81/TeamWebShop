using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopLibrary;
using TeamWebShop.Infrastructure.ModelBinders;

namespace TeamWebShop.Infrastructure.BinderProviders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Cart) ?
                new CartModelBinder() : null;
        }
    }
}
