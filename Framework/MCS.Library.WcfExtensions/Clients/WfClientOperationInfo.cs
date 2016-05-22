using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions.Inspectors
{
    public class WfClientOperationInfo
    {
        private WfClientParameterCollection _Parameters = null;
        private OperationDescription _OperationDescription = null;
        private string _Name = string.Empty;

        internal WfClientOperationInfo()
        {
        }

        internal WfClientOperationInfo(OperationDescription opDesp, object[] parameters)
        {
            this.Fill(opDesp, parameters);
        }

        internal void Fill(OperationDescription opDesp, object[] parameters)
        {
            opDesp.NullCheck("opDesp");

            this._OperationDescription = opDesp;
            this.FillParametersInfo(this._OperationDescription, parameters);
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
        }

        public OperationDescription OperationDescription
        {
            get
            {
                return this._OperationDescription;
            }
        }

        public WfClientParameterCollection Parameters
        {
            get
            {
                if (this._Parameters == null)
                    this._Parameters = new WfClientParameterCollection();

                return this._Parameters;
            }
        }

        private void FillParametersInfo(OperationDescription opDesp, object[] parameters)
        {
            this._Name = opDesp.Name;

            for (int i = 0; i < opDesp.Messages[0].Body.Parts.Count; i++)
            {
                string paramName = opDesp.Messages[0].Body.Parts[i].Name;
                object paramVal = parameters[i];

                MessagePartDescription mp = opDesp.Messages[0].Body.Parts[i];

                WfClientParameter parameter = new WfClientParameter()
                {
                    Name = mp.Name,
                    Type = mp.Type,
                    Value = parameters[i]
                };

                this.Parameters.Add(parameter);
            }
        }
    }
}
