using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    public class MaterialModelHelper
    {
        private MaterialAdapter _Adapter = null;

        public static MaterialModelHelper GetInstance(string connName)
        {
            return new MaterialModelHelper(connName);
        }

        private MaterialModelHelper(string connName)
        {
            this._Adapter = MaterialAdapter.GetInstance(connName);
        }

        public MaterialModel Load(string materialID)
        {
            Material material = this.Adapter.LoadMaterialByMaterialID(materialID);
            MaterialModel result = null;

            material.IsNotNull(m => result = new MaterialModel(m));

            return result;
        }

        public MaterialModelCollection Update(MaterialModelCollection materials)
        {
            this.Adapter.SaveDeltaMaterials(materials.ToDeltaMaterials());
            materials.ForEach(m => m.Status = MaterualModelStatus.Unmodified);

            return materials;
        }

        public MaterialModelCollection LoadByResourceID(string resourceID)
        {
            MaterialList materials = this.Adapter.LoadMaterialsByResourceID(resourceID);

            return new MaterialModelCollection(materials);
        }

        public MaterialAdapter Adapter
        {
            get
            {
                return this._Adapter;
            }
        }
    }
}
