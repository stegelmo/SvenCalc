using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SvenCalc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(options =>
            {
                // Det här var ju lite annorlunda mot gamla Asp.net.
                // Mao lite annorlunda i Asp.net Core. I en "riktig" applikation hade jag nog lagt in dessa i resursfiler. Stannar dock här i detta exempel.
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((v, p) => $"'{v}' är ett ogiltigt tal för {p}.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((v) => $"Var god ange ett värde för {v}.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((v) => $"{v} är ogiltigt.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => $"Var god ange ett värde.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((v) => $"'{v}' måste vara ett nummer.");

                // TBD
                //UnknownValueIsInvalidAccessor The supplied value is invalid for { 0}.
                //ValueIsInvalidAccessor The value '{0}' is invalid.
                //MissingRequestBodyRequiredValueAccessor A non - empty request body is required.
                //NonPropertyAttemptedValueIsInvalidAccessor The value '{0}' is not valid.
                //NonPropertyUnknownValueIsInvalidAccessor The supplied value is invalid.
                //NonPropertyValueMustBeANumberAccessor The field must be a number.
            });

            // Förhindra CSRF
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Vi kör på svenska (för valideringstexterna)
            SetSwedish(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
        private void SetSwedish(IApplicationBuilder app)
        {
            var defaultCulture = new CultureInfo("sv-se");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);
        }
    }
}
