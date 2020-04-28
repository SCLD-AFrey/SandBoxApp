using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using SandBox.Data;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class CardViewModel
    {
        public class MetaData : IMetadataProvider<CardViewModel>
        {
            void IMetadataProvider<CardViewModel>.BuildMetadata
                (MetadataBuilder<CardViewModel> p_builder)
            {
                //p_builder.Property(p_x => p_x.SelectedPerson).OnPropertyChangedCall(p_x => p_x.PersonSelectedChanged());

                p_builder.CommandFromMethod(p_x => p_x.OnCompleteRecordDragDropCommand()).CommandName("CompleteRecordDragDropCommand");
                p_builder.CommandFromMethod(p_x => p_x.OnUpdateButtonCommand()).CommandName("UpdateButtonCommand");

            }
        }

        #region Constructors

        protected CardViewModel()
        {
            session = new UnitOfWork();

            AvailablePersonCollection = new XPCollection<Person_Person>(session);
            SelectedPerson = new Person_Person(session);
            SelectedPersonCollection = new ObservableCollection<Person_Person>();
        }

        public static CardViewModel Create()
        {
            return ViewModelSource.Create(() => new CardViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }
        public virtual XPCollection<Person_Person> AvailablePersonCollection { get; set; }
        public virtual Person_Person SelectedPerson { get; set; }
        public virtual ObservableCollection<Person_Person> SelectedPersonCollection { get; set; }
        public virtual XPCollection<Person_Selected> SelectedSavePersonCollection { get; set; }

        #endregion

        #region Methods

        public void OnCompleteRecordDragDropCommand()
        {

            SelectedPersonCollection.Add(SelectedPerson);
        }

        public void OnUpdateButtonCommand()
        {
            SelectedSavePersonCollection = new XPCollection<Person_Selected>(session);
            foreach (var p in SelectedPersonCollection)
            {
                Person_Selected ps = new Person_Selected(session)
                {
                    FirstName = p.FirstName,
                    PersonObj = p,
                    LastName = p.LastName,
                    MiddleName = p.MiddleName
                };

                SelectedSavePersonCollection.Add(ps);

            }

            session.Save(SelectedSavePersonCollection);
        }

        #endregion
    }
}