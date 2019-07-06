using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fluent
{

    public class FluentNodeHandlerAttribute : Attribute
    {
        public Type FluentNodeHandlerType { get; private set; }
        public bool IsExplicit { get; private set; }
        public FluentNodeHandlerAttribute(Type fluentNodeHandlerType, bool isExplicit = false) 
        {
            FluentNodeHandlerType = fluentNodeHandlerType;
            IsExplicit = IsExplicit;
        }
    }
}
