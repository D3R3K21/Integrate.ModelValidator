﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.ModelValidation
{
    public class RequestModelTypeAttribute : Attribute
    {
        RequestModelTypeAttribute() 
        {        
        }
        RequestModelTypeAttribute(Type obj)
        {

        }
    }
}