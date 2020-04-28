using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Linq;
using DevExpress.Xpo;
using SandBox.Data;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class QueryViewModel
    {
        public class MetaData : IMetadataProvider<QueryViewModel>
        {
            void IMetadataProvider<QueryViewModel>.BuildMetadata
                (MetadataBuilder<QueryViewModel> p_builder)
            {


            }
        }

        #region Constructors

        protected QueryViewModel()
        {
            session = new UnitOfWork();

            PersonQuery = new XPQuery<Person_Person>(session);

            PersonQueryFilter = 
                from p in PersonQuery
                where (p.PersonType == "EM")
                orderby p.LastName
                select p;
        }

        public static QueryViewModel Create()
        {
            return ViewModelSource.Create(() => new QueryViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }


        public virtual XPQuery<Person_Person> PersonQuery { get; set; }
        public virtual IOrderedQueryable<Person_Person> PersonQueryFilter { get; set; }

        #endregion

        #region Methods



        #endregion
    }
}