﻿using System;
using AutoMapper;

namespace AmBlitzCore.AutoMapper
{
    public class AutoMapToAttribute: AutoMapAttributeBase
    {
        public AutoMapToAttribute(params Type[] targetTypes)
            :base(targetTypes)
        {

        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {

            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(type, targetType);
            }
        }
    }
}