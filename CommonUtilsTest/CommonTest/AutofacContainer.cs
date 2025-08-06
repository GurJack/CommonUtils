//using System;
//using System.Collections.Generic;
//using Autofac;
//using Autofac.Core;
//using Microsoft.Extensions.Configuration;

//namespace CommonUtils
//{
//    /// <summary>
//    /// IoC based on <see cref="Autofac"/>.
//    /// </summary>
//    public static class AutofacContainer
//    {
//        private static readonly IContainer Container;

//        /// <summary>
//        /// Static constructor.
//        /// </summary>
//        static AutofacContainer()
//        {
//            // Add the configuration to the ConfigurationBuilder.
//            var config = new ConfigurationBuilder();

//            //// config.AddJsonFile comes from Microsoft.Extensions.Configuration.Json
//            config.AddJsonFile("autofac.json");

//            var module = new Autofac.Configuration.ConfigurationModule(config.Build());
//            var builder = new ContainerBuilder();
//            builder.RegisterModule(module);

//            Container = builder.Build();
//        }

//        /// <summary>
//        /// Resolve a service.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T Resolve<T>()
//        {
//            return Container.Resolve<T>();
//        }

//        /// <summary>
//        /// Resolve a service.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T Resolve<T>(IDictionary<string, object> @params)
//        {
//            var parameters = new List<Parameter>(@params.Count);
//            foreach (var parameter in @params)
//            {
//                parameters.Add(new NamedParameter(parameter.Key, parameter.Value));
//            }

//            return Container.Resolve<T>(parameters);
//        }

//        /// <summary>
//        /// Resolve a service.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T Resolve<T>(IDictionary<Type, object> @params)
//        {
//            var parameters = new List<Parameter>(@params.Count);
//            foreach (var parameter in @params)
//            {
//                parameters.Add(new TypedParameter(parameter.Key, parameter.Value));
//            }

//            return Container.Resolve<T>(parameters);
//        }
//    }
//}
