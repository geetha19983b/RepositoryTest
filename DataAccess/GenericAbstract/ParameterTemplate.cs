using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosys.FoundationLibrary.DataAccess.GenericAbstract
{
    public class ParamterTemplate
    {
        public string ParamterName { get; set; }
        public dynamic ParamterValue { get; set; }
        public Type ParameterType { get; set; }
        public string ParameterDirection { get; set; }

        public ParamterTemplate(string ParamterName, dynamic ParamterValue, Type ParameterType, string ParameterDirection)
        {
            this.ParamterName = ParamterName;
            this.ParamterValue = ParamterValue;
            this.ParameterType = ParameterType;
            this.ParameterDirection = ParameterDirection;

        }
    }
}
