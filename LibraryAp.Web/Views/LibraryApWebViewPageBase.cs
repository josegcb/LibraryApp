using Abp.Web.Mvc.Views;

namespace LibraryAp.Web.Views
{
    public abstract class LibraryApWebViewPageBase : LibraryApWebViewPageBase<dynamic>
    {

    }

    public abstract class LibraryApWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected LibraryApWebViewPageBase()
        {
            LocalizationSourceName = LibraryApConsts.LocalizationSourceName;
        }
    }
}