﻿using Nancy;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;

namespace IDPJobManager.Bootstrapper.Mef.Extensions
{

    static class TypeExtensions
    {

        /// <summary>
        /// Returns <c>true</c> if the type references Nancy in some way.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ReferencesNancy(this Type type)
        {
            Debug.Assert(type != null);

            // does type's assembly's references contain Nancy assembly?
            return type.Assembly.GetReferencedAssemblies()
                .Prepend(type.Assembly.GetName())
                .Any(r => r.Name == typeof(INancyEngine).Assembly.GetName().Name);
        }

    }

}
