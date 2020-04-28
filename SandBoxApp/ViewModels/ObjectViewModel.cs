using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo;
using SandBox.Data;
using SandBoxApp.Enumerations;

namespace SandBoxApp.ViewModels
{
    [MetadataType(typeof(MetaData))]
    public class ObjectViewModel
    {
        public class MetaData : IMetadataProvider<ObjectViewModel>
        {
            void IMetadataProvider<ObjectViewModel>.BuildMetadata
                (MetadataBuilder<ObjectViewModel> p_builder)
            {
                p_builder.Property(p_x => p_x.SelectedPerson).OnPropertyChangedCall(p_x => p_x.OnSelectedPersonChanged());
                p_builder.CommandFromMethod(p_x => p_x.OnCreateButtonCommand()).CommandName("CreateButtonCommand");
                p_builder.CommandFromMethod(p_x => p_x.OnSaveButtonCommand()).CommandName("SaveButtonCommand");

            }
        }

        #region Constructors

        protected ObjectViewModel()
        {
            session = new UnitOfWork();

            AvailablePersonCollection = new XPCollection<Person_Person>(session);

        }

        public static ObjectViewModel Create()
        {
            return ViewModelSource.Create(() => new ObjectViewModel());
        }

        #endregion

        #region Fields and Properties

        public virtual UnitOfWork session { get; set; }
        public virtual XPCollection<Person_Person> AvailablePersonCollection { get; set; }
        public virtual Person_Person SelectedPerson { get; set; }

        public virtual XPCollection<Person_PersonPhone> PhoneCollection { get; set; }

        public virtual enumPersonType PersonType { get; }



        #endregion

        #region Methods

        public void OnSelectedPersonChanged()
        {


        }
        public void OnSaveButtonCommand()
        {
            if (SelectedPerson.BusinessEntityID <= 0)
            {
                Person_BusinessEntity businessEntity = new Person_BusinessEntity(session)
                {
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };
                session.CommitChanges();
                SelectedPerson.BusinessEntityID = businessEntity.BusinessEntityID;
            }
            SelectedPerson.ModifiedDate = DateTime.Now;
            
            session.CommitChanges();
        }
        public void OnCreateButtonCommand()
        {
            SelectedPerson = new Person_Person(session);
        }

        #endregion
    }
}