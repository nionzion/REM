using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Ninject;

namespace REM
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use.
        /// Note: Must be called as soon as your application starts up to ensure all services can be found.
        /// </summary>
        public static void Setup()
        {
            //Bind all required view models
            BindViewModels();
        }

        private static void BindViewModels()
        {       
            //Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

        }


        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
