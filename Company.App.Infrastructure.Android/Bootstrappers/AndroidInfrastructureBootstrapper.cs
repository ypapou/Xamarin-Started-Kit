﻿using Company.App.Common.Bootstrappers;
using Company.App.Infrastructure.Dialogs;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;

namespace Company.App.Infrastructure.Bootstrappers
{
    public class AndroidInfrastructureBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register<IUserDialog>(() => new UserDialog(), Reuse.Singleton);
        }
    }
}