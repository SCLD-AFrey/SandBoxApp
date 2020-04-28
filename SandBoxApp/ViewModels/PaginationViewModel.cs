using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using SandBox.Data;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class PaginationViewModel
    {
        public class MetaData : IMetadataProvider<PaginationViewModel>
        {
            void IMetadataProvider<PaginationViewModel>.BuildMetadata
                (MetadataBuilder<PaginationViewModel> p_builder)
            {
                //p_builder.Property(p_x => p_x.SelectedPerson).OnPropertyChangedCall(p_x => p_x.PersonSelectedChanged());
            }
        }

        #region Constructors

        protected PaginationViewModel()
        {
            session = new UnitOfWork();

            AvailablePersonCollection = new XPCollection<Person_Person>(session);
            SelectedPerson = new Person_Person(session);
        }

        public static PaginationViewModel Create()
        {
            return ViewModelSource.Create(() => new PaginationViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }
        public virtual XPCollection<Person_Person> AvailablePersonCollection { get; set; }
        public virtual Person_Person SelectedPerson { get; set; }


        #endregion

        #region Methods



        #endregion
    }
}