using FastKala.Application.ViewModels.Orders;

namespace FastKala.Utilities;

public class CookieHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<CartItemsViewModel>? GetCartCookie()
    {
        try
        {
            string? coockieString = null;
            string cookieKey = "shopping-cart";
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                coockieString = httpContext.Request.Cookies[cookieKey];
            }

            if (coockieString != null)
            {
                string cookieValue = coockieString;
                string[] loginVlues = cookieValue.Split(",");
                if (loginVlues.Length == 5)
                {
                    //return new LoginCookie()
                    //{
                    //    Id = loginVlues[1],
                    //    UserId = Convert.ToInt32(loginVlues[2]),
                    //    LoginDatetime = Convert.ToInt64(loginVlues[3]),
                    //    Theme = loginVlues[4],
                    //    NavbarColor = loginVlues[5]
                    //};
                }
            }
            return null;
        }
        catch
        {
            return null;

        }

    }

    public bool SetCartCookie(Guid guid)
    {
        try
        {
            string cookieKey = "shopping-cart";
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieKey, guid.ToString());
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool RemoveCartCookie()
    {
        try
        {
            string cookieKey = "shopping-cart";
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieKey);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}