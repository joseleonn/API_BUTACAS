using Service.Services;
using Service.IServices;
namespace API_BUTACAS
{
    public class CompositeRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped<ISeatService, SeatService>();

            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<IMailService, MailService>();



        }
    }
}
