﻿using Application;
using Infrastructure.CosmosDb;
using MassTransit;

namespace WebApi.ExtensionMethods;

public static class StartupBootstrapper
{
    public static WebApplicationBuilder Configure(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        
        ApplicationBootstrapper.ConfigureApplication(builder.Services, builder.Configuration);
        CosmosDbBootstrapper.ConfigureCosmosDb(builder.Services, builder.Configuration);
        
        //TODO: implement switch depending on environment
        builder.Services.ConfigureMassTransitWithRabbitMq(mediatorCfg: ApplicationBootstrapper.ConfigureMediator);
        
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        return app;
    }

    public static void RunApp(this WebApplication app) => app.Run();
    
    private static IServiceCollection ConfigureMassTransitWithRabbitMq(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator>? busCfg = null,
        Action<IMediatorRegistrationConfigurator>? mediatorCfg = null)
        => services.ConfigureMassTransit(
            x => x.UsingRabbitMq((context, cfg) =>
            {
                UseBusKillSwitch(cfg);

                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            }),
            busCfg,
            mediatorCfg);
    
    private static IServiceCollection ConfigureMassTransit(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator> busTransportCfg,
        Action<IBusRegistrationConfigurator>? busCfg = null,
        Action<IMediatorRegistrationConfigurator>? mediatorCfg = null)
    {
        services.AddMassTransit(x =>
        {
            if (busCfg != null)
            {
                busCfg(x);
            }

            x.SetKebabCaseEndpointNameFormatter();

            busTransportCfg(x);
        });

        if (mediatorCfg != null)
        {
            services.AddMediator(x => mediatorCfg(x));
        }

        services.AddOptions<MassTransitHostOptions>()
            .Configure(options => options.WaitUntilStarted = true);

        return services;
    }
    
    private static void UseBusKillSwitch(IBusFactoryConfigurator cfg)
    {
        cfg.UseKillSwitch(options => options
            .SetActivationThreshold(16)
            .SetTripThreshold(0.1)
            .SetRestartTimeout(s: 40));
    }
}