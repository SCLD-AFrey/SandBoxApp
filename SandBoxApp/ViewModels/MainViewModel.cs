using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using SandBox.Data;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class MainViewModel
    {
        public class MetaData : IMetadataProvider<MainViewModel>
        {
            void IMetadataProvider<MainViewModel>.BuildMetadata
                (MetadataBuilder<MainViewModel> p_builder)
            {

                p_builder.CommandFromMethod(p_x => p_x.OnAvailablePersonDoubleClickCommand())
                    .CommandName("AvailablePersonDoubleClickCommand");
                p_builder.CommandFromMethod(p_x => p_x.OnSelectedPersonDetailsChangedCommand())
                    .CommandName("SelectedPersonDetailsChangedCommand()()");

            }
        }

        #region Constructors

        protected MainViewModel()
        {
            session = new UnitOfWork();

            AvailablePersonCollection = new XPCollection<Person_Person>(session);
            AvailablePersonCollection.Filter = 
                new BinaryOperator(
                    nameof(Person_Person.PersonType),
                    "EM",
                    BinaryOperatorType.NotEqual
                    );

            SelectedPersonCollection = new ObservableCollection<Person_Person>();
            AvailablePersonCursor = new XPCursor(
                session, 
                typeof(Person_Person)
                );
        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }
        public virtual XPCollection<Person_Person> AvailablePersonCollection { get; set; }
        public virtual ObservableCollection<Person_Person> SelectedPersonCollection { get; set; }
        public virtual XPCursor AvailablePersonCursor { get; set; }
        public virtual Person_Person SelectedPerson { get; set; }
        public virtual Person_Person SelectedPersonDetails { get; set; }
        public virtual HumanResources_Employee EmployeeDetails { get; set; }

        #endregion

        #region Methods

        public void OnAvailablePersonDoubleClickCommand()
        {
            if (SelectedPerson != null)
            {
                SelectedPersonCollection.Add(SelectedPerson);
                AvailablePersonCollection.Remove(SelectedPerson);
            }

            SelectedPerson = null;

        }

        public void OnSelectedPersonDetailsChangedCommand()
        {
            
        }

        #endregion
    }
}