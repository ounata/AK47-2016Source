using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    [Serializable]
    [DataContract]
    public class MaterialModel : Material
    {
        public MaterialModel()
        {
        }

        public MaterialModel(Material material)
        {
            material.NullCheck("material");
            material.Fill(this);
        }

        [DataMember]
        [NoMapping]
        public MaterualModelStatus Status
        {
            get;
            set;
        }
    }

    [Serializable]
    public class MaterialModelCollection : EditableDataObjectCollectionBase<MaterialModel>
    {
        public MaterialModelCollection()
        {
        }

        public MaterialModelCollection(MaterialList materials)
        {
            materials.ForEach(m => this.Add(new MaterialModel(m)));
        }

        internal DeltaMaterialList ToDeltaMaterials()
        {
            DeltaMaterialList delta = new DeltaMaterialList();

            foreach (MaterialModel materialModel in this)
            {
                switch (materialModel.Status)
                {
                    case MaterualModelStatus.Inserted:
                        delta.Inserted.Append(materialModel);
                        break;
                    case MaterualModelStatus.Updated:
                        delta.Updated.Append(materialModel);
                        break;
                    case MaterualModelStatus.Deleted:
                        delta.Deleted.Append(materialModel);
                        break;
                }
            }

            return delta;
        }
    }
}
