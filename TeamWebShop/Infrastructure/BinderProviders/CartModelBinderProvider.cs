using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopLibrary;
using TeamWebShop.Infrastructure.ModelBinders;

namespace TeamWebShop.Infrastructure.BinderProviders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            IHttpContextAccessor contextAccessor = context.Services.GetRequiredService<IHttpContextAccessor>();
            return context.Metadata.ModelType == typeof(Cart) ?
                new CartModelBinder(contextAccessor) : null;
        }
    }
}
