﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string? message) : base(message)
        {
        }

        public static void ThrowIfNull(object? @object, string exceptionMessage)
        {
            if (@object is null)
                throw new NotFoundException(exceptionMessage);
        }
    }
}
