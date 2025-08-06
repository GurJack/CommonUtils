using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using CommonUtils.Helpers;

namespace CommonUtils.Expressions
{
    /// <summary>
    /// The dynamic type provider for expression parsing.
    /// </summary>
    public class DynamicTypeProvider : DefaultDynamicLinqCustomTypeProvider
    {
        private HashSet<Type> _customTypes;

        /// <summary>
        /// Returns all custom types.
        /// </summary>
        /// <returns></returns>
        public override HashSet<Type> GetCustomTypes()
        {
            if (_customTypes != null)
            {
                return _customTypes;
            }

            //todo: what is directory name in common case? may be need condition
            _customTypes = new HashSet<Type>(FindTypesMarkedWithDynamicLinqTypeAttribute(TypeHelper.GetAllAssemblies()));
            return _customTypes;
        }

        /// <summary>
        /// Adds the custom type.
        /// </summary>
        /// <param name="type"></param>
        public void AddCustomType(Type type)
        {
            if (_customTypes == null)
            {
                _customTypes = GetCustomTypes();
            }

            if (!_customTypes.Contains(type))
            {
                _customTypes.Add(type);
            }
        }
    }
}